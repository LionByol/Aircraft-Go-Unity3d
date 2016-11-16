using UnityEditor;
using UnityEngine;
using System.Collections;

public class WP8NativeSettingsEditorMenu : MonoBehaviour {

#if UNITY_EDITOR

	//--------------------------------------
	//  GENERAL
	//--------------------------------------
	
	[MenuItem("Window/Stan`s Assets/WP8 Native/Edit Settings")]
	public static void Edit() {
		Selection.activeObject = WP8NativeSettings.Instance;
	}

	//--------------------------------------
	//  GETTING STARTED
	//--------------------------------------
	
	[MenuItem("Window/Stan`s Assets/WP8 Native/Documentation/Getting Started/Plugin Set Up")]
	public static void WP8GSPluginSetUp() {
		Application.OpenURL("https://unionassets.com/wp8-native/setup-and-update-263");
	}

	//--------------------------------------
	//  In-App Purchases
	//--------------------------------------
		
	[MenuItem("Window/Stan`s Assets/WP8 Native/Documentation/In-App Purchases/Setup")]
	public static void WP8IASetup() {
		Application.OpenURL("https://unionassets.com/wp8-native/setup-224");
	}
		
	[MenuItem("Window/Stan`s Assets/WP8 Native/Documentation/In-App Purchases/Coding Guidelines")]
	public static void WP8IACodingGuidelines() {
		Application.OpenURL("https://unionassets.com/wp8-native/coding-guidelines-coding-guidelines-264");
	}

	//--------------------------------------
	//  More Features
	//--------------------------------------
			
	[MenuItem("Window/Stan`s Assets/WP8 Native/Documentation/More Features/Poups and Pre-loaders")]
	public static void WP8MFPoupsAndPreLoaders() {
		Application.OpenURL("https://unionassets.com/wp8-native/showing-message-pop-up-246");
	}

	//--------------------------------------
	//  FAQ
	//--------------------------------------
		
	[MenuItem("Window/Stan`s Assets/WP8 Native/Documentation/FAQ")]
	public static void WP8FAQ() {
		string url = "https://unionassets.com/wp8-native/manual#faq";
		Application.OpenURL(url);
	}

	#endif
}
