////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GK_Leaderboard  {


	public GK_ScoreCollection SocsialCollection =  new GK_ScoreCollection();
	public GK_ScoreCollection GlobalCollection =  new GK_ScoreCollection();

	public Dictionary<string, int> currentPlayerRank =  new Dictionary<string, int>();

	private GK_LeaderBoardInfo _info;
	

	public GK_Leaderboard(string leaderboardId) {
		_info =   new GK_LeaderBoardInfo();
		_info.Identifier = leaderboardId;
	}

	public void UpdateMaxRange(int MR) {
		_info.MaxRange = MR;
	}


	public string id {
		get {
			return _info.Identifier;
		}
	}


	public GK_Score GetCurrentPlayerScore(GK_TimeSpan timeSpan, GK_CollectionType collection) {
		string key = timeSpan.ToString() + "_" + collection.ToString();
		if(currentPlayerRank.ContainsKey(key)) {
			int rank = currentPlayerRank[key];
			return GetScore(rank, timeSpan, collection);
		} else {
			return null;
		}
		
	}
	
	public void UpdateCurrentPlayerRank(int rank, GK_TimeSpan timeSpan, GK_CollectionType collection) {
		string key = timeSpan.ToString() + "_" + collection.ToString();
		if(currentPlayerRank.ContainsKey(key)) {
			currentPlayerRank[key] = rank;
		} else {
			currentPlayerRank.Add(key, rank);
		}
	}


	public GK_Score GetScore(int rank, GK_TimeSpan scope, GK_CollectionType collection) {

		GK_ScoreCollection col = GlobalCollection;
		
		switch(collection) {
		case GK_CollectionType.GLOBAL:
			col = GlobalCollection;
			break;
		case GK_CollectionType.FRIENDS:
			col = SocsialCollection;
			break;
		}
		



		Dictionary<int, GK_Score> scoreDict = col.AllTimeScores;
		
		switch(scope) {
		case GK_TimeSpan.ALL_TIME:
			scoreDict = col.AllTimeScores;
			break;
		case GK_TimeSpan.TODAY:
			scoreDict = col.TodayScores;
			break;
		case GK_TimeSpan.WEEK:
			scoreDict = col.WeekScores;
			break;
		}



		if(scoreDict.ContainsKey(rank)) {
			return scoreDict[rank];
		} else {
			return null;
		}

	}

	public void UpdateScore(GK_Score s) {

		GK_ScoreCollection col = GlobalCollection;

		switch(s.collection) {
		case GK_CollectionType.GLOBAL:
			col = GlobalCollection;
			break;
		case GK_CollectionType.FRIENDS:
			col = SocsialCollection;
			break;
		}




		Dictionary<int, GK_Score> scoreDict = col.AllTimeScores;

		switch(s.timeSpan) {
		case GK_TimeSpan.ALL_TIME:
			scoreDict = col.AllTimeScores;
			break;
		case GK_TimeSpan.TODAY:
			scoreDict = col.TodayScores;
			break;
		case GK_TimeSpan.WEEK:
			scoreDict = col.WeekScores;
			break;
		}


		if(scoreDict.ContainsKey(s.GetRank())) {
			scoreDict[s.GetRank()] = s;
		} else {
			scoreDict.Add(s.GetRank(), s);
		}
	}

	public GK_LeaderBoardInfo Info {
		get {
			return _info;
		}
	}
}

