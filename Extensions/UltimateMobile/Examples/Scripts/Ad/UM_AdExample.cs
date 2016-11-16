﻿////////////////////////////////////////////////////////////////////////////////
//  
// @module Google Ads Unity SDK 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using UnityEngine;
using System.Collections;

public class UM_AdExample : MonoBehaviour {





	private GUIStyle style;
	private GUIStyle style2;

	private int bannerId1 = 0;
	private int bannerId2 = 0;
	private bool isLoadInt = false;




	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Start() {

		//Required
		UM_AdManager.OnInterstitialLoaded += HandleOnInterstitialLoaded;
		UM_AdManager.OnInterstitialLoadFail += HandleOnInterstitialLoadFail;
		UM_AdManager.OnInterstitialClosed += HandleOnInterstitialClosed;
		UM_AdManager.instance.Init();


		UM_ExampleStatusBar.text = "Unified ad example scene loaded";

		InitStyles();

	}

	void HandleOnInterstitialClosed ()
	{
		Debug.Log ("HandleOnInterstitialClosed");

		UM_AdManager.OnInterstitialClosed -= HandleOnInterstitialClosed;
	}

	void HandleOnInterstitialLoadFail ()
	{
		Debug.Log ("HandleOnInterstitialLoadFail");

		UM_AdManager.OnInterstitialLoaded -= HandleOnInterstitialLoaded;
		UM_AdManager.OnInterstitialLoadFail -= HandleOnInterstitialLoadFail;
		UM_AdManager.OnInterstitialClosed -= HandleOnInterstitialClosed;
	}

	void HandleOnInterstitialLoaded ()
	{
		Debug.Log ("HandleOnInterstitialLoaded");

		UM_AdManager.OnInterstitialLoaded -= HandleOnInterstitialLoaded;
		UM_AdManager.OnInterstitialLoadFail -= HandleOnInterstitialLoadFail;
	}
	
	
	private void InitStyles () {
		style =  new GUIStyle();
		style.normal.textColor = Color.white;
		style.fontSize = 16;
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperLeft;
		style.wordWrap = true;
		
		
		style2 =  new GUIStyle();
		style2.normal.textColor = Color.white;
		style2.fontSize = 12;
		style2.fontStyle = FontStyle.Italic;
		style2.alignment = TextAnchor.UpperLeft;
		style2.wordWrap = true;
	}


	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	void OnGUI() {


		float StartY = 20;
		float StartX = 10;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40), "Interstisal Example", style);

		StartY+= 40;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Start Interstitial Ad")) {
			UM_AdManager.instance.StartInterstitialAd();
		}

		StartX+= 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Load Interstitial Ad")) {
				UM_AdManager.instance.LoadInterstitialAd();
			isLoadInt = true;

		}

		GUI.enabled  = false;
		if(isLoadInt != false) {
				GUI.enabled  = true;
		}
		
		
		StartX+= 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Show Interstitial Ad")) {
			UM_AdManager.instance.ShowInterstitialAd();
			isLoadInt = false;
		}




		GUI.enabled  = true;
		StartY+= 80;
		StartX = 10;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40), "Banners Example", style);

		GUI.enabled = false;
		if(bannerId1 == 0) {
			GUI.enabled  = true;
		}


		StartY+= 40;
		StartX = 10;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Banner Top Left")) {
			bannerId1 = UM_AdManager.instance.CreateAdBanner(TextAnchor.UpperLeft);
		}

		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Banner Top Center")) {
			bannerId1 = UM_AdManager.instance.CreateAdBanner(TextAnchor.UpperCenter);
		}

		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Banner Top Right")) {
			bannerId1 = UM_AdManager.instance.CreateAdBanner(TextAnchor.UpperRight);
		}

		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Banner Bottom Left")) {
			bannerId1 = UM_AdManager.instance.CreateAdBanner(TextAnchor.LowerLeft);
		}

		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Banner Bottom Center")) {
			bannerId1 = UM_AdManager.instance.CreateAdBanner(TextAnchor.LowerCenter);
		}

		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Banner Bottom Right")) {
			bannerId1 = UM_AdManager.instance.CreateAdBanner(TextAnchor.LowerRight);
		}



		GUI.enabled  = false;
		if(bannerId1 != 0) {
			if(UM_AdManager.instance.IsBannerLoaded(bannerId1)) {
				GUI.enabled  = true;
			}
		}


		StartY+= 80;
		StartX = 10;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Refresh")) {
			UM_AdManager.instance.RefreshBanner(bannerId1);
		}

	


		GUI.enabled  = false;
		if(bannerId1 != 0) {
			if(UM_AdManager.instance.IsBannerLoaded(bannerId1) && UM_AdManager.instance.IsBannerOnScreen(bannerId1)) {
				GUI.enabled  = true;
			}
		}
		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Hide")) {
			UM_AdManager.instance.HideBanner(bannerId1);

		}


		GUI.enabled  = false;
		if(bannerId1 != 0) {
			if(UM_AdManager.instance.IsBannerLoaded(bannerId1) && !UM_AdManager.instance.IsBannerOnScreen(bannerId1)) {
				GUI.enabled  = true;
			}
		}
		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Show")) {
			UM_AdManager.instance.ShowBanner(bannerId1);
		}





		GUI.enabled  = false;
		if(bannerId1 != 0) {
			GUI.enabled  = true;
		}

		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Destroy")) {
			UM_AdManager.instance.DestroyBanner(bannerId1);
			bannerId1 = 0;

		}
		GUI.enabled  = true;


		StartY+= 80;
		StartX = 10;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40), "Banner 2", style);

		GUI.enabled = false;
		if(bannerId2 == 0) {
			GUI.enabled  = true;
		}

		StartY+= 40;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Create Banner 2")) {
			bannerId2 = UM_AdManager.instance.CreateAdBanner(TextAnchor.LowerCenter);
		}



		GUI.enabled  = false;
		if(bannerId2 != 0) {
			if(UM_AdManager.instance.IsBannerLoaded(bannerId2)) {
				GUI.enabled  = true;
			}
		}
		
		
		StartY+= 80;
		StartX = 10;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Refresh")) {
			UM_AdManager.instance.RefreshBanner(bannerId2);
		}
		
		
		
		
		GUI.enabled  = false;
		if(bannerId2 != 0) {
			if(UM_AdManager.instance.IsBannerLoaded(bannerId2) && UM_AdManager.instance.IsBannerOnScreen(bannerId2)) {
				GUI.enabled  = true;
			}
		}
		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Hide")) {
			UM_AdManager.instance.HideBanner(bannerId2);
			
		}
		
		
		GUI.enabled  = false;
		if(bannerId2 != 0) {
			if(UM_AdManager.instance.IsBannerLoaded(bannerId2) && !UM_AdManager.instance.IsBannerOnScreen(bannerId2)) {
				GUI.enabled  = true;
			}
		}
		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Show")) {
			UM_AdManager.instance.ShowBanner(bannerId2);
		}
		
		
		
		
		
		GUI.enabled  = false;
		if(bannerId2 != 0) {
			GUI.enabled  = true;
		}
		
		StartX += 170;
		if(GUI.Button(new Rect(StartX, StartY, 150, 50), "Destroy")) {
			UM_AdManager.instance.DestroyBanner(bannerId2);
			bannerId2 = 0;
			
		}

		GUI.enabled  = true;

	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------



	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
