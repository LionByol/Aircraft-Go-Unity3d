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

public class UM_GameServiceExample : BaseIOSFeaturePreview {
	
	public int hiScore = 100;

	
	private string leaderBoardId =  "LeaderBoardSample_1";
	private string leaderBoardId2 = "LeaderBoardSample_2";


	private string TEST_ACHIEVEMENT_1_ID = "AchievementSample_1";
	private string TEST_ACHIEVEMENT_2_ID = "AchievementSample_2";
	


	//--------------------------------------
	// INITIALIZE
	//--------------------------------------


	


	void Awake() {
		UM_GameServiceManager.instance.Connect();

		UM_ExampleStatusBar.text = "Connecting To Game Service";

		UM_GameServiceManager.OnPlayerConnected += OnPlayerConnected;
		UM_GameServiceManager.OnPlayerDisconnected += OnPlayerDisconnected;


		if(UM_GameServiceManager.instance.ConnectionSate == UM_ConnectionState.CONNECTED) {
			OnPlayerConnected();
		}

	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	void OnGUI() {


		if(UM_GameServiceManager.instance.ConnectionSate == UM_ConnectionState.CONNECTED) {
			GUI.enabled = true;
		} else {
			GUI.enabled = false;
		}



		UpdateToStartPos();

		if(UM_GameServiceManager.instance.player != null) {
			GUI.Label(new Rect(100, 10, Screen.width, 40), "ID: " + UM_GameServiceManager.instance.player.PlayerId);
			GUI.Label(new Rect(100, 20, Screen.width, 40), "Name: " +  UM_GameServiceManager.instance.player.Name);
	

		

			if(UM_GameServiceManager.instance.player.Avatar != null) {
				GUI.DrawTexture(new Rect(10, 10, 75, 75), UM_GameServiceManager.instance.player.Avatar);
			}
		}

		StartY+= YLableStep;
		StartY+= YLableStep;
		StartY+= YLableStep;


		GUI.Label(new Rect(StartX, StartY, Screen.width, 40), "GameCneter Leaderboards", style);


		StartY+= YLableStep;
		
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leaderboards")) {
			UM_GameServiceManager.instance.ShowLeaderBoardsUI();
			UM_ExampleStatusBar.text = "Showing Leader Boards UI";
		}


		StartY+= YButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leader Board1")) {
			UM_GameServiceManager.instance.ShowLeaderBoardUI(leaderBoardId);
			UM_ExampleStatusBar.text = "Showing " + leaderBoardId + " UI";
		}

		StartX += XButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Score LB 1")) {
			hiScore++;
			UM_GameServiceManager.instance.SubmitScore(leaderBoardId, hiScore);
			UM_ExampleStatusBar.text = "Score " + hiScore + " Submited to " + leaderBoardId;
		}
		
		StartX += XButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Score LB 1")) {
			long s = UM_GameServiceManager.instance.GetCurrentPlayerScore(leaderBoardId);
			UM_ExampleStatusBar.text = "GetCurrentPlayerScore from  " + leaderBoardId + " is: " + s;
		}


		StartX = XStartPos;
		StartY+= YButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leader Board2")) {
			UM_GameServiceManager.instance.ShowLeaderBoardUI(leaderBoardId2);
			UM_ExampleStatusBar.text = "Showing " + leaderBoardId2 + " UI";

		}



		StartX += XButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Score LB2")) {
			hiScore++;
			UM_GameServiceManager.instance.SubmitScore(leaderBoardId2, hiScore);
			UM_ExampleStatusBar.text = "Score " + hiScore + " Submited to " + leaderBoardId2;
		}


		StartX += XButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Score LB 2")) {
			long s = UM_GameServiceManager.instance.GetCurrentPlayerScore(leaderBoardId2);
			UM_ExampleStatusBar.text = "GetCurrentPlayerScore from  " + leaderBoardId2 + " is: " + s;
		}

		StartX = XStartPos;
		StartY+= YButtonStep;
		StartY+= YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40), "Achievements", style);

		StartY+= YLableStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Achievements")) {
			UM_GameServiceManager.instance.ShowAchievementsUI();
			UM_ExampleStatusBar.text = "Showing Achievements UI";
		}

		StartX += XButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Reset Achievements")) {

			UM_GameServiceManager.instance.ResetAchievements();
			UM_ExampleStatusBar.text = "Al acievmnets reseted";
		}

		StartX += XButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Achievements1")) {
			UM_GameServiceManager.instance.UnlockAchievement(TEST_ACHIEVEMENT_1_ID);
			UM_ExampleStatusBar.text = "Achievement " + TEST_ACHIEVEMENT_1_ID  + " Reported";

		}

		StartX += XButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Increment Achievements2")) {
			UM_GameServiceManager.instance.IncrementAchievement(TEST_ACHIEVEMENT_2_ID, 20f);
			UM_ExampleStatusBar.text = "Achievement " + TEST_ACHIEVEMENT_2_ID  + " Oncremented by 20%";

		}
		


	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------


	private void OnPlayerConnected() {
		UM_ExampleStatusBar.text = "Player Connected";
	}
	

	private void OnPlayerDisconnected() {
		UM_ExampleStatusBar.text = "Player Disconnected";
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
