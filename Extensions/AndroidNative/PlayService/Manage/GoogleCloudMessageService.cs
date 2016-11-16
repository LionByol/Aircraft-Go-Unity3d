////////////////////////////////////////////////////////////////////////////////
//  
// @module Android Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections.Generic;

public class GoogleCloudMessageService : SA_Singleton<GoogleCloudMessageService> {


	//Actions
	public static event Action<string> ActionCouldMessageLoaded 						 						= delegate {};
	public static event Action<GP_GCM_RegistrationResult> ActionCMDRegistrationResult  						= delegate {};
	public static event Action<string, Dictionary<string, object>, bool> ActionGameThriveNotificationReceived	= delegate {};


	private string _lastMessage = string.Empty;
	private string _registrationId = string.Empty;


	
	
	
	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------

	public void InitOneSignalNotifications() {
		OneSignal.Init(AndroidNativeSettings.Instance.GameThriveAppID, AndroidNativeSettings.Instance.GCM_SenderId, HandleNotification);
	}

	// Gets called when the player opens the notification.
	private static void HandleNotification(string message, Dictionary<string, object> additionalData, bool isActive) {
		ActionGameThriveNotificationReceived (message, additionalData, isActive);
	}

	public void InitPushNotifications() {
		AN_NotificationProxy.InitPushNotifications (
			AndroidNativeSettings.Instance.PushNotificationIcon == null ? string.Empty : AndroidNativeSettings.Instance.PushNotificationIcon.name,
		    AndroidNativeSettings.Instance.PushNotificationSound == null ? string.Empty : AndroidNativeSettings.Instance.PushNotificationSound.name,
		    AndroidNativeSettings.Instance.EnableVibrationPush, AndroidNativeSettings.Instance.ShowPushWhenAppIsForeground,
			AndroidNativeSettings.Instance.ReplaceOldNotificationWithNew);
	}

	public void InitPushNotifications(string icon, string sound, bool enableVibrationPush, bool showWhenAppForeground, bool replaceOldNotificationWithNew) {
		AN_NotificationProxy.InitPushNotifications (icon, sound,enableVibrationPush, showWhenAppForeground, replaceOldNotificationWithNew);
	}

	public void InitParsePushNotifications() {
		AN_NotificationProxy.InitParsePushNotifications (AndroidNativeSettings.Instance.ParseAppId, AndroidNativeSettings.Instance.DotNetKey);
	}

	public void RgisterDevice() {
		AN_NotificationProxy.GCMRgisterDevice(AndroidNativeSettings.Instance.GCM_SenderId);
	}

	public void LoadLastMessage() {
		AN_NotificationProxy.GCMLoadLastMessage();
	}

	public void RemoveLastMessageInfo() {
		AN_NotificationProxy.GCMRemoveLastMessageInfo();
	}


	
	//--------------------------------------
	// GET / SET
	//--------------------------------------
	
	public string registrationId {
		get {
			return _registrationId;
		}
	}

	public string lastMessage {
		get {
			return _lastMessage;
		}
	}
	
	
	//--------------------------------------
	// EVENTS
	//--------------------------------------

	private void OnLastMessageLoaded(string data) {
		_lastMessage = data;
		ActionCouldMessageLoaded(lastMessage);

	}

	
	private void OnRegistrationReviced(string regId) {
		_registrationId = regId;

		ActionCMDRegistrationResult(new GP_GCM_RegistrationResult(_registrationId));
	}
	
	private void OnRegistrationFailed() {
		ActionCMDRegistrationResult(new GP_GCM_RegistrationResult());
	}
	
	
	
}
