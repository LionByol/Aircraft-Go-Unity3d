using UnityEngine;
using System;
using System.Collections;

public class UM_GameServiceManager : SA_Singleton<UM_GameServiceManager> {
	

	public static event Action OnPlayerConnected = delegate {};
	public static event Action OnPlayerDisconnected = delegate {};


	private bool _IsInitedCalled = false;
	private bool _IsDataLoaded = false;

	private int dataEventsCount = 0;
	private int currentEventsCount = 0;


	private GameServicePlayerTemplate _player = null ;
	private UM_ConnectionState _ConnectionSate = UM_ConnectionState.UNDEFINED;



	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}


	private void Init() {

		_IsInitedCalled = true;

		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			dataEventsCount = UltimateMobileSettings.Instance.Leaderboards.Count + 1;


			foreach(UM_Achievement achievment in UltimateMobileSettings.Instance.Achievements) {
				GameCenterManager.RegisterAchievement(achievment.IOSId);
			}

			break;
		case RuntimePlatform.Android:
			dataEventsCount = 2;
			break;
		}


		GooglePlayConnection.ActionPlayerConnected += OnAndroidPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnAndroidPlayerDisconnected;
		GooglePlayManager.ActionAchievementsLoaded += OnGooglePlayServiceDataLoaded;
		GooglePlayManager.ActionLeaderboardsLoaded += OnGooglePlayServiceDataLoaded;



		GameCenterManager.OnAuthFinished += OnAuthFinished;
		GameCenterManager.OnAchievementsLoaded += OnGameCenterServiceDataLoaded;
		GameCenterManager.OnPlayerScoreLoaded += OnGameCenterServiceDataLoaded;
	}


	//--------------------------------------
	// Metods
	//--------------------------------------

	public void Connect() {


		if(!_IsInitedCalled) {
			Init();
		}

		if(_ConnectionSate == UM_ConnectionState.CONNECTED || _ConnectionSate == UM_ConnectionState.CONNECTING) {
			return;
		}

		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.Init();
			break;
		case RuntimePlatform.Android:
			GooglePlayConnection.instance.connect();
			break;
		}

		_ConnectionSate = UM_ConnectionState.CONNECTING;
	}

	public void Disconnect() {
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:

			break;
		case RuntimePlatform.Android:
			GooglePlayConnection.instance.disconnect();
			break;
		}

	}


	public void ShowAchievementsUI() {
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ShowAchievements();
			break;
		case RuntimePlatform.Android:
			GooglePlayManager.instance.ShowAchievementsUI();
			break;
		}
	}
	
	public void ShowLeaderBoardsUI() {
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ShowLeaderboards();
			break;
		case RuntimePlatform.Android:
			GooglePlayManager.instance.ShowLeaderBoardsUI();
			break;
		}
	}


	public void ShowLeaderBoardUI(string id) {
		ShowLeaderBoardUI(UltimateMobileSettings.Instance.GetLeaderboardById(id));
	}

	public void ShowLeaderBoardUI(UM_Leaderboard leaderboard) {
		if(leaderboard == null) {
			return;
		}
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ShowLeaderboard(leaderboard.IOSId);
			break;
		case RuntimePlatform.Android:
			GooglePlayManager.instance.ShowLeaderBoardById(leaderboard.AndroidId);
			break;
		}
	}


	public void SubmitScore(string LeaderboardId, long score) {
		SubmitScore(UltimateMobileSettings.Instance.GetLeaderboardById(LeaderboardId), score);
	}

	public void SubmitScore(UM_Leaderboard leaderboard, long score) {
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ReportScore(score, leaderboard.IOSId);
			break;
		case RuntimePlatform.Android:
			GooglePlayManager.instance.SubmitScoreById(leaderboard.AndroidId, score);
			break;
		}
	}


	public void RevealAchievement(string id) {
		RevealAchievement(UltimateMobileSettings.Instance.GetAchievementById(id));
	}

	public void RevealAchievement(UM_Achievement achievement) {
		switch(Application.platform) {
		
		case RuntimePlatform.Android:
			GooglePlayManager.instance.RevealAchievementById(achievement.AndroidId);
			break;
		}
	}

	[Obsolete("ReportAchievement is deprecated, please use UnlockAchievement instead.")]
	public void ReportAchievement(string id) {
		UnlockAchievement(id);
	}

	[Obsolete("ReportAchievement is deprecated, please use UnlockAchievement instead.")]
	public void ReportAchievement(UM_Achievement achievement) {
		ReportAchievement(achievement);
	}


	public void UnlockAchievement(string id) {
		UM_Achievement achievement = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievement == null) {
			Debug.LogError("Achievment not found with id: " + id);
			return;
		}

		UnlockAchievement(achievement);
	}


	private void UnlockAchievement(UM_Achievement achievement) {


		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.SubmitAchievement(100f, achievement.IOSId);
			break;
		case RuntimePlatform.Android:
			GooglePlayManager.instance.UnlockAchievementById(achievement.AndroidId);
			break;
		}
	}


	public void IncrementAchievement(string id,  float percentages) {
		UM_Achievement achievement = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievement == null) {
			Debug.LogError("Achievment not found with id: " + id);
			return;
		}

		IncrementAchievement(achievement, percentages);
	}


	public void IncrementAchievement(UM_Achievement achievement, float percentages) {
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.SubmitAchievement(percentages, achievement.IOSId);
			break;
		case RuntimePlatform.Android:

			GPAchievement a = GooglePlayManager.instance.GetAchievement(achievement.AndroidId);
			if(a != null) {
				if(a.type == GPAchievementType.TYPE_INCREMENTAL) {
					int steps = Mathf.CeilToInt((a.totalSteps / 100f) * percentages);
					GooglePlayManager.instance.IncrementAchievementById(achievement.AndroidId, steps);
				} else {
					GooglePlayManager.instance.UnlockAchievementById(achievement.AndroidId);
				}
			}
			break;
		}
	}


	public void ResetAchievements() {
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ResetAchievements();
			break;
		case RuntimePlatform.Android:
			GooglePlayManager.instance.ResetAllAchievements();
			break;
		}
	}


	public float GetAchievementProgress(string id) {
		UM_Achievement achievement = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievement == null) {
			Debug.LogError("Achievment not found with id: " + id);
			return 0f;
		}

		return GetAchievementProgress(achievement);
	}
	
	public float GetAchievementProgress(UM_Achievement achievement) {
		if(achievement == null) {
			return 0f;
		}

		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			return GameCenterManager.GetAchievementProgress(achievement.IOSId);
		case RuntimePlatform.Android:
			
			GPAchievement a = GooglePlayManager.instance.GetAchievement(achievement.AndroidId);
			if(a != null) {
				if(a.type == GPAchievementType.TYPE_INCREMENTAL) {
					return  (a.currentSteps / a.totalSteps) * 100f;
				} else {
					if(a.state == GPAchievementState.STATE_UNLOCKED) {
						return 100f;
					} else {
						return 0f;
					}
				}
			}
			break;
		}
		
		return 0f;
	}
	


	public long GetCurrentPlayerScore(string leaderBoardId) {
		return GetCurrentPlayerScore(UltimateMobileSettings.Instance.GetLeaderboardById(leaderBoardId));
	} 

	public long GetCurrentPlayerScore(UM_Leaderboard leaderboard) {
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			GK_Leaderboard board =  GameCenterManager.GetLeaderboard(leaderboard.IOSId);
			if(board != null) {
				GK_Score score = board.GetCurrentPlayerScore(GK_TimeSpan.ALL_TIME, GK_CollectionType.GLOBAL);
				if (score != null) {
					return score.GetLongScore();
				}
			} 
			break;
		case RuntimePlatform.Android:
			GPLeaderBoard gBoard =  GooglePlayManager.instance.GetLeaderBoard(leaderboard.AndroidId);
			if(gBoard != null) {
				GPScore score = gBoard.GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.GLOBAL);
				if(score != null) {
					return score.score;
				}
			} 
			break;
		}

		return 0;
	} 



	public int GetCurrentPlayerRank(string leaderBoardId) {
		return GetCurrentPlayerRank(UltimateMobileSettings.Instance.GetLeaderboardById(leaderBoardId));
	} 
	
	public int GetCurrentPlayerRank(UM_Leaderboard leaderboard) {
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			GK_Leaderboard board =  GameCenterManager.GetLeaderboard(leaderboard.IOSId);
			if(board != null) {
				return board.GetCurrentPlayerScore(GK_TimeSpan.ALL_TIME, GK_CollectionType.GLOBAL).rank;
			} else {
				return 0;
			}
		case RuntimePlatform.Android:
			GPLeaderBoard gBoard =  GooglePlayManager.instance.GetLeaderBoard(leaderboard.AndroidId);
			if(gBoard != null) {
				return gBoard.GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.GLOBAL).rank;
			} else {
				return 0;
			}
		}

		return 0;
	} 


	
	//--------------------------------------
	// Get / Set
	//--------------------------------------


	public UM_ConnectionState ConnectionSate {
		get {
			return _ConnectionSate;
		}
	}

	public GameServicePlayerTemplate player {
		get {
			return _player;
		}
	}
	

	//--------------------------------------
	// Events
	//--------------------------------------

	private void OnServiceConnected() {

		if(_IsDataLoaded) {
			OnAllLoaded();
			return;
		}


		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			foreach(UM_Leaderboard leaderboard in UltimateMobileSettings.Instance.Leaderboards) {
				GameCenterManager.LoadCurrentPlayerScore(leaderboard.IOSId, GK_TimeSpan.ALL_TIME, GK_CollectionType.GLOBAL);
			}
			break;
		case RuntimePlatform.Android:
			GooglePlayManager.instance.LoadAchievements();
			GooglePlayManager.instance.LoadLeaderBoards();
			break;
		}
	}

	private void OnGooglePlayServiceDataLoaded(GooglePlayResult result) {
		if (result.isSuccess) {
			currentEventsCount++;
			if(currentEventsCount == dataEventsCount) {
				OnAllLoaded();
			}
		}		
	}

	private void OnGameCenterServiceDataLoaded(ISN_Result e) {
		//if (e.IsSucceeded) {
			currentEventsCount++;
			if(currentEventsCount == dataEventsCount) {
				OnAllLoaded();
			}
		//}		
	}

	private void OnAllLoaded() {
		_ConnectionSate = UM_ConnectionState.CONNECTED;
		_player =  new GameServicePlayerTemplate(GameCenterManager.Player, GooglePlayManager.instance.player);


		OnPlayerConnected();
	}


	//--------------------------------------
	// IOS Events
	//--------------------------------------



	private void OnAuthFinished (ISN_Result res) {
		if(res.IsSucceeded) {
			OnServiceConnected();
		} else {
			_ConnectionSate = UM_ConnectionState.DISCONNECTED;
			OnPlayerDisconnected();
		}
	}

	//--------------------------------------
	// Android Events
	//--------------------------------------

	private void OnAndroidPlayerConnected() {
		OnServiceConnected();
	}
	
	private void OnAndroidPlayerDisconnected() {
		_ConnectionSate = UM_ConnectionState.DISCONNECTED;
		OnPlayerDisconnected();
	}


}
