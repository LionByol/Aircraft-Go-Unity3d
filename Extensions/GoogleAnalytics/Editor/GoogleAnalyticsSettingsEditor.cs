﻿
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(GoogleAnalyticsSettings))]
public class GoogleAnalyticsSettingsEditor : Editor {



	GUIContent acountNameLabel = new GUIContent("Account Name [?]:", "Name of Google Analytics Account");
	GUIContent appNameLabel = new GUIContent("App Name [?]:", "For your own use and organization.");

	GUIContent testingModeLabel = new GUIContent("Testing [?]:", "This account will be used if testing mode enabled");
	GUIContent prodModeLabel = new GUIContent("Production [?]:", "This account will be used if testing mode disabled.");

	GUIContent appVersionLabel = new GUIContent("App Version [?]:", "Your application build version.");
	GUIContent appTrackingIdLabel = new GUIContent("Tracking Id [?]:", "The ID that distinguishes to which Google Analytics property to send data.");


	GUIContent newLevelTrackingLabel = new GUIContent("Levels [?]:", "Screen Hit will be sent automaticaly when new level is loaded");
	GUIContent exTrackingLabel = new GUIContent("Exceptions [?]:", "Application exceptions reports will be sent automatically ");
	GUIContent systemInfoTrackingLabel = new GUIContent("SystemInfo [?]:", "System info will be automatically submitted on first launch ");
	GUIContent quitTrackingLabel = new GUIContent("App Quit [?]:", "Automatically track app quit.");
	GUIContent pauseTrackingLabel = new GUIContent("App [?]:", "Automatically track when app goes background and start / end session on this event ");

	GUIContent levelPostfix = new GUIContent("Level Postfix [?]:", "Postfix for loaded scene name ");
	GUIContent levelPrefix = new GUIContent("Level Prefix [?]:", "Prefix for loaded scene name");

	GUIContent sdkVersion = new GUIContent("SDK Version [?]", "This is Plugin version.  If you have problems or compliments please include this so we know exactly what version to look out for.");
	GUIContent supportEmail = new GUIContent("Support [?]", "If you have any technical quastion, feel free to drop an e-mail");

	GUIContent UseHttpsLabel = new GUIContent("Use HTTPS [?]", "Enable data send  over SSL");
	GUIContent StringEscape  = new GUIContent("String Escape [?]", "Enable All strings  escaping using WWW.EscapeURL, Escaping of strings is safer, but adds additional RAM consummation");
	GUIContent EditorAnalytics  = new GUIContent("Send From Editor [?]", "Enable sending analytics while you testing your game in Unity Editor");


	GUIContent EnableCachingLabel  = new GUIContent("Enable Caching [?]", "When Internet Connection is not available event hits will be saved and sanded when connection is recovered.");
	GUIContent EnableQueueTimeLabel  = new GUIContent("Enable Queue Time [?]", "Queue Time to report time when hit occurred. But values greater than four hours may lead to hits not being processed by Google.");
	


	GUIContent UsePlaterSettingLabel  = new GUIContent("Use Player Settings [?]", "Use Player setting as app name and version info");


	private GoogleAnalyticsSettings settings;

	public override void OnInspectorGUI() {
		settings = target as GoogleAnalyticsSettings;

		GUI.changed = false;

		if(settings.IsDisabled) {
			GUI.enabled = false;
		} else {
			GUI.enabled = true;
		}



		


	



		Messages();
		EditorGUILayout.Space();
		Accounts();
		EditorGUILayout.Space();
		GeneralOptions();
		EditorGUILayout.Space();
		AdvancedTracking();
		EditorGUILayout.Space();
		AutoTracking();
		EditorGUILayout.Space();
		AboutGUI();

		ButtonsGUI();

		if(GUI.changed) {
			DirtyEditor();
		}
	}

	private void Messages() {
		#if UNITY_WEBPLAYER  
		EditorGUILayout.HelpBox("Sending analytics in Editor is disabled because you using Web Player Mode. To find out more about analytics usgae in web player, please click the link bellow", MessageType.Warning);
		EditorGUILayout.BeginHorizontal();
		
		EditorGUILayout.Space();
		if(GUILayout.Button("Using With Web Player",  GUILayout.Width(150))) {
			Application.OpenURL("http://goo.gl/5lbHLd");
		}

		EditorGUILayout.EndHorizontal();

		#endif

	}


	private void Accounts() {
		EditorGUILayout.HelpBox("(Required) Platfroms and Accounts", MessageType.None);
		if (settings.accounts.Count == 0) {
			EditorGUILayout.HelpBox("Setup at least one Google Analytics Account", MessageType.Error);
		}


		GoogleAnalyticsSettings.Instance.showAccounts = EditorGUILayout.Foldout(GoogleAnalyticsSettings.Instance.showAccounts, "Google Analytics Account");
		if(GoogleAnalyticsSettings.Instance.showAccounts) {


			foreach(GAProfile profile in GoogleAnalyticsSettings.Instance.accounts) {
			
				EditorGUI.indentLevel = 1;
				EditorGUILayout.BeginVertical (GUI.skin.box);
				profile.IsOpen = EditorGUILayout.Foldout(profile.IsOpen, profile.Name);
				if(profile.IsOpen) {
					EditorGUI.indentLevel = 2;


					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(acountNameLabel);
					profile.Name	 	= EditorGUILayout.TextField(profile.Name);
					EditorGUILayout.EndHorizontal();



					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(appTrackingIdLabel);
					profile.TrackingID	 	= EditorGUILayout.TextField(profile.TrackingID);
					EditorGUILayout.EndHorizontal();



					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.Space();

					if(GUILayout.Button("Set For All Platfroms",  GUILayout.Width(150))) {
						int options = EditorUtility.DisplayDialogComplex(
							"Setting Account",
							"Setting " + profile.Name + " for all platfroms",
							"Set As Testing",
							"Cancel",
							"Set As Production"
							);
						
						switch(options) {
						case 0:

							foreach(RuntimePlatform platfrom in (RuntimePlatform[]) System.Enum.GetValues(typeof(RuntimePlatform)) ) {
								GoogleAnalyticsSettings.Instance.SetProfileIndexForPlatfrom(platfrom, GoogleAnalyticsSettings.Instance.GetProfileIndex(profile), true);
							}

							break;
						case 2:
							foreach(RuntimePlatform platfrom in (RuntimePlatform[]) System.Enum.GetValues(typeof(RuntimePlatform)) ) {
								GoogleAnalyticsSettings.Instance.SetProfileIndexForPlatfrom(platfrom, GoogleAnalyticsSettings.Instance.GetProfileIndex(profile), false);
							}
							break;


						}

						DirtyEditor();
					}


					if(GUILayout.Button("Remove",  GUILayout.Width(80))) {
						GoogleAnalyticsSettings.Instance.RemoveProfile(profile);
						return;
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.Space();
					
				}

				EditorGUILayout.EndVertical ();

			}


			EditorGUI.indentLevel = 0;
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.Space();
			if(GUILayout.Button("Add New Account",  GUILayout.Width(120))) {
				GoogleAnalyticsSettings.Instance.AddProfile(new GAProfile());
			}

			EditorGUILayout.EndHorizontal();

		}
		
		GoogleAnalyticsSettings.Instance.showPlatfroms = EditorGUILayout.Foldout(GoogleAnalyticsSettings.Instance.showPlatfroms, "Platfroms Settings");
		if(GoogleAnalyticsSettings.Instance.showPlatfroms) {

			if (settings.accounts.Count == 0) {
				EditorGUILayout.HelpBox("Setup at least one Google Analytics Profile", MessageType.Error);
			} else {
				foreach(RuntimePlatform platfrom in (RuntimePlatform[]) System.Enum.GetValues(typeof(RuntimePlatform)) ) {
					
					EditorGUI.indentLevel = 1;
					EditorGUILayout.BeginVertical (GUI.skin.box);
					
					
					EditorGUILayout.BeginHorizontal();
					EditorGUI.indentLevel = 0;
					EditorGUILayout.LabelField(platfrom.ToString());
					EditorGUILayout.EndHorizontal();


					EditorGUI.indentLevel = 1;


					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(prodModeLabel);
					EditorGUI.BeginChangeCheck();
					
					
					int index = GoogleAnalyticsSettings.Instance.GetProfileIndexForPlatfrom(platfrom, false);
					index = EditorGUILayout.Popup(index, GoogleAnalyticsSettings.Instance.GetProfileNames());
					
					
					if(EditorGUI.EndChangeCheck()) {
						GoogleAnalyticsSettings.Instance.SetProfileIndexForPlatfrom(platfrom, index, false);
						DirtyEditor();
					}
					EditorGUILayout.EndHorizontal();



					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(testingModeLabel);
					EditorGUI.BeginChangeCheck();
					
					
					 index = GoogleAnalyticsSettings.Instance.GetProfileIndexForPlatfrom(platfrom, true);
					index = EditorGUILayout.Popup(index, GoogleAnalyticsSettings.Instance.GetProfileNames());
					
					
					if(EditorGUI.EndChangeCheck()) {
						GoogleAnalyticsSettings.Instance.SetProfileIndexForPlatfrom(platfrom, index, true);
						DirtyEditor();
					}

					EditorGUILayout.EndHorizontal();




				

					
					
					
					EditorGUILayout.EndVertical ();
				}
			}

		}


		GoogleAnalyticsSettings.Instance.showTestingMode = EditorGUILayout.Foldout(GoogleAnalyticsSettings.Instance.showTestingMode, "Testing Mode");
		if(GoogleAnalyticsSettings.Instance.showTestingMode) {
			if (settings.accounts.Count == 0) {
				EditorGUILayout.HelpBox("Setup at least one Google Analytics Profile", MessageType.Error);
			} else {
				EditorGUILayout.HelpBox("If Testing mode is enabled, testing account will be used on all platfroms. You will also get build warning if building with testing mode enabled. Make sure you will disable it with the production build", MessageType.Info);

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(testingModeLabel);
				settings.IsTestingModeEnabled	 	= EditorGUILayout.Toggle(settings.IsTestingModeEnabled);
				EditorGUILayout.EndHorizontal();

			}
		}



	}



	private void GeneralOptions() {
		EditorGUILayout.Space();
		EditorGUILayout.HelpBox("(Required) Application Info", MessageType.None);


		if(settings.UsePlayerSettingsForAppInfo) {
			GUI.enabled = false;

			settings.UpdateVersionAndName();
			
		}
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(appNameLabel);
		settings.AppName	 	= EditorGUILayout.TextField(settings.AppName);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(appVersionLabel);
		settings.AppVersion	 	= EditorGUILayout.TextField(settings.AppVersion);
		EditorGUILayout.EndHorizontal();

		GUI.enabled = true;
		settings.UsePlayerSettingsForAppInfo = EditorGUILayout.Toggle(UsePlaterSettingLabel, settings.UsePlayerSettingsForAppInfo);


		EditorGUILayout.Space();

	}

	



	private void AdvancedTracking() {
		EditorGUILayout.HelpBox("(Optional) Edit the advanced tracking parameters", MessageType.None);
		GoogleAnalyticsSettings.Instance.showAdvancedParams = EditorGUILayout.Foldout(GoogleAnalyticsSettings.Instance.showAdvancedParams, "Advanced Tracking");
		if(GoogleAnalyticsSettings.Instance.showAdvancedParams) {
			settings.UseHTTPS = EditorGUILayout.Toggle(UseHttpsLabel, settings.UseHTTPS);
			settings.StringEscaping = EditorGUILayout.Toggle(StringEscape, settings.StringEscaping);
			settings.EditorAnalytics = EditorGUILayout.Toggle(EditorAnalytics, settings.EditorAnalytics);
		}

		GoogleAnalyticsSettings.Instance.showCParams = EditorGUILayout.Foldout(GoogleAnalyticsSettings.Instance.showCParams, "Requests Caching");
		if(GoogleAnalyticsSettings.Instance.showCParams) {
		
			settings.IsRequetsCachingEnabled = EditorGUILayout.Toggle(EnableCachingLabel,   settings.IsRequetsCachingEnabled);
			settings.IsQueueTimeEnabled 	 = EditorGUILayout.Toggle(EnableQueueTimeLabel, settings.IsQueueTimeEnabled);
		}
	}


	private void AutoTracking() {
		EditorGUILayout.HelpBox("(Optional) Edit the automatic tracking parameters", MessageType.None);
		GoogleAnalyticsSettings.Instance.showAdditionalParams = EditorGUILayout.Foldout(GoogleAnalyticsSettings.Instance.showAdditionalParams, "Automatic Tracking");
		if (GoogleAnalyticsSettings.Instance.showAdditionalParams) {


			settings.AutoExceptionTracking = EditorGUILayout.Toggle(exTrackingLabel, settings.AutoExceptionTracking);
			settings.SubmitSystemInfoOnFirstLaunch = EditorGUILayout.Toggle(systemInfoTrackingLabel, settings.SubmitSystemInfoOnFirstLaunch);
			settings.AutoAppResumeTracking = EditorGUILayout.Toggle(pauseTrackingLabel, settings.AutoAppResumeTracking);
			settings.AutoAppQuitTracking = EditorGUILayout.Toggle(quitTrackingLabel, settings.AutoAppQuitTracking);

			settings.AutoLevelTracking = EditorGUILayout.Toggle(newLevelTrackingLabel, settings.AutoLevelTracking);
			
			if(settings.AutoLevelTracking) {
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(levelPrefix);
				EditorGUILayout.LabelField(levelPostfix);
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				settings.LevelPrefix =  EditorGUILayout.TextField(settings.LevelPrefix);
				settings.LevelPostfix = EditorGUILayout.TextField(settings.LevelPostfix);
				EditorGUILayout.EndHorizontal();
			} 

		}
		EditorGUILayout.Space();
	}

	private void ButtonsGUI() {
		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.Space();
		if(GUILayout.Button("Refresh Client Id",  GUILayout.Width(120))) {
			PlayerPrefs.DeleteKey(GoogleAnalytics.GOOGLE_ANALYTICS_CLIENTID_PREF_KEY);
		}


		GUI.enabled = true;

		Color c = GUI.color;
		string text = "";
		if(GoogleAnalyticsSettings.Instance.IsDisabled) {
			text = "Enable Analytics";
			GUI.color = Color.green;
		} else {
			text = "Disable Analytics";
			GUI.color = Color.red;
		}


		if(GUILayout.Button(text, GUILayout.Width(120))) {
			GoogleAnalyticsSettings.Instance.IsDisabled = !GoogleAnalyticsSettings.Instance.IsDisabled;
		}

		GUI.color = c;


		EditorGUILayout.EndHorizontal();
	}


	private void AboutGUI() {
		
		EditorGUILayout.HelpBox("About the Plugin", MessageType.None);
		
		SelectableLabelField(sdkVersion, GoogleAnalyticsSettings.VERSION_NUMBER);
		SelectableLabelField(supportEmail, "stans.assets@gmail.com");
		
		
	}
	
	private void SelectableLabelField(GUIContent label, string value) {
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(label, GUILayout.Width(180), GUILayout.Height(16));
		EditorGUILayout.SelectableLabel(value, GUILayout.Height(16));
		EditorGUILayout.EndHorizontal();
	}



	private static void DirtyEditor() {
		#if UNITY_EDITOR
		EditorUtility.SetDirty(GoogleAnalyticsSettings.Instance);
		#endif
	}
	
	
}
