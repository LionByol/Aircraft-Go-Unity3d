using UnityEngine;
using System.Collections;

public class WP8FeaturesExample : WPNFeaturePreview {

	public static WP8NativePreviewBackButton back = null;

	void Awake() {
		if(back == null) {
			back = WP8NativePreviewBackButton.Create();
		}
	}


	void OnGUI() {
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40), "Basic Features", style);
		
		StartY+= YLableStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "In App Purchasing")) {
			Application.LoadLevel("In_AppPurchases");
		}
		
		StartX += XButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Native Pop Ups")) {
			Application.LoadLevel("PopUpExample");
		}
	}

}
