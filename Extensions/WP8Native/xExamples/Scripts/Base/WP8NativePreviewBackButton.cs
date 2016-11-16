using UnityEngine;
using System.Collections;

public class WP8NativePreviewBackButton : WPNFeaturePreview {


	private string initalSceneName = "scene";

	public static WP8NativePreviewBackButton Create() {
		return new GameObject("BackButton").AddComponent<WP8NativePreviewBackButton>();
	} 


	void Awake() {
		DontDestroyOnLoad(gameObject);
		initalSceneName = Application.loadedLevelName;
	}


	void OnGUI() {
		float bw = 120;
		float x = Screen.width - bw * 1.2f ;
		float y = bw * 0.2f;


		if(!Application.loadedLevelName.Equals(initalSceneName)) {
			Color customColor = GUI.color;
			GUI.color = Color.green;

			if(GUI.Button(new Rect(x, y, bw, bw * 0.4f), "Back")) {
				Application.LoadLevel(initalSceneName);
			}

			GUI.color = customColor;

		}
	}
	 


}
