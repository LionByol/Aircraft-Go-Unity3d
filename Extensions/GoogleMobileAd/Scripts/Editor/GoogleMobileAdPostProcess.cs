#define CODE_DISABLED

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;

public class GoogleMobileAdPostProcess  {
	

	[PostProcessBuild(49)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {


		#if UNITY_IPHONE && !CODE_DISABLED
		string StoreKit = "StoreKit.framework";
		if(!ISDSettings.Instance.frameworks.Contains(StoreKit)) {
			ISDSettings.Instance.frameworks.Add(StoreKit);
		}


		string CoreTelephony = "CoreTelephony.framework";
		if(!ISDSettings.Instance.frameworks.Contains(CoreTelephony)) {
			ISDSettings.Instance.frameworks.Add(CoreTelephony);
		}


		string AdSupport = "AdSupport.framework";
		if(!ISDSettings.Instance.frameworks.Contains(AdSupport)) {
			ISDSettings.Instance.frameworks.Add(AdSupport);
		}
	

		string MessageUI = "MessageUI.framework";
		if(!ISDSettings.Instance.frameworks.Contains(MessageUI)) {
			ISDSettings.Instance.frameworks.Add(MessageUI);
		}

		string EventKit = "EventKit.framework";
		if(!ISDSettings.Instance.frameworks.Contains(EventKit)) {
			ISDSettings.Instance.frameworks.Add(EventKit);
		}

		string EventKitUI = "EventKitUI.framework";
		if(!ISDSettings.Instance.frameworks.Contains(EventKitUI)) {
			ISDSettings.Instance.frameworks.Add(EventKitUI);
		}

		/*
		string linkerFlasg = "-ObjC";
		if(!ISDSettings.Instance.linkFlags.Contains(linkerFlasg)) {
			ISDSettings.Instance.linkFlags.Add(linkerFlasg);
		}
		*/
		#endif
	}

}
