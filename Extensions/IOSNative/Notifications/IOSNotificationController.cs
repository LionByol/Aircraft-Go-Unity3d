//#define PUSH_ENABLED
////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using System;
using System.Collections;
using System.Collections.Generic;
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
using System.Runtime.InteropServices;
#endif


#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
using UnityEngine;
#else

#if UNITY_IOS
using UnityEngine.iOS;
using UnityEngine;
#endif


#endif


public class IOSNotificationController : ISN_Singleton<IOSNotificationController> {


	private static IOSNotificationController _instance;

	private static int _AllowedNotificationsType = -1;

	private ISN_LocalNotification _LaunchNotification = null;
	

	//Actions
	public static event Action<IOSNotificationDeviceToken> OnDeviceTokenReceived = delegate {};
	public static event Action<ISN_Result>  OnNotificationScheduleResult = delegate {};
	public static event Action<int>  OnNotificationSettingsInfoResult = delegate {};

	public static event Action<ISN_LocalNotification>  OnLocalNotificationReceived = delegate {};

	#if (UNITY_IPHONE && !UNITY_EDITOR && PUSH_ENABLED) || SA_DEBUG_MODE
	[NonSerialized]
	public Action<RemoteNotification> OnRemoteNotificationReceived = delegate {};
	#endif



	private const string PP_ID_KEY = "IOSNotificationControllerKey_ID";

	#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
	[DllImport ("__Internal")]
	private static extern void _ISN_ScheduleNotification (int time, string message, bool sound, string nId, int badges, string data);
	
	[DllImport ("__Internal")]
	private static extern  void _ISN_ShowNotificationBanner (string title, string message);
	
	[DllImport ("__Internal")]
	private static extern void _ISN_CancelNotifications();


	[DllImport ("__Internal")]
	private static extern void _ISN_RequestNotificationPermissions();

	[DllImport ("__Internal")]
	private static extern void _ISN_CancelNotificationById(string nId);

	[DllImport ("__Internal")]
	private static extern  void _ISN_ApplicationIconBadgeNumber (int badges);


	[DllImport ("__Internal")]
	private static extern void _ISN_RegisterForRemoteNotifications(int types);


	[DllImport ("__Internal")]
	private static extern void _ISN_RequestNotificationSettings();

	#endif

	

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	

	void Awake() {

		DontDestroyOnLoad(gameObject);

		#if UNITY_IPHONE || UNITY_IOS

		#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
		if( NotificationServices.localNotificationCount > 0) {
			LocalNotification n = NotificationServices.localNotifications[0];
			
			ISN_LocalNotification notif = new ISN_LocalNotification(DateTime.Now, n.alertBody, true);
			
			int id = 0;
			if(n.userInfo.Contains("AlarmKey")) {
				id = System.Convert.ToInt32(n.userInfo["AlarmKey"]);
			}
			
			if(n.userInfo.Contains("data")) {
				notif.SetData(System.Convert.ToString(n.userInfo["data"]));
			}
			notif.SetId(id);

			_LaunchNotification = notif;
		}
		#else

			#if UNITY_IOS
			if( UnityEngine.iOS.NotificationServices.localNotificationCount > 0) {
				UnityEngine.iOS.LocalNotification n = UnityEngine.iOS.NotificationServices.localNotifications[0];
				
				ISN_LocalNotification notif = new ISN_LocalNotification(DateTime.Now, n.alertBody, true);
				
				int id = 0;
				if(n.userInfo.Contains("AlarmKey")) {
					id = System.Convert.ToInt32(n.userInfo["AlarmKey"]);
				}
				
				if(n.userInfo.Contains("data")) {
					notif.SetData(System.Convert.ToString(n.userInfo["data"]));
				}
				notif.SetId(id);
					
				_LaunchNotification = notif;
			}
			#endif

		#endif


		#endif
	}



	#if (UNITY_IPHONE && !UNITY_EDITOR && PUSH_ENABLED) || SA_DEBUG_MODE
	void FixedUpdate() {
		if(NotificationServices.remoteNotificationCount > 0) {
			foreach(var rn in NotificationServices.remoteNotifications) {
				if(!IOSNativeSettings.Instance.DisablePluginLogs) 
					UnityEngine.Debug.Log("Remote Noti: " + rn.alertBody);
				//IOSNotificationController.instance.ShowNotificationBanner("", rn.alertBody);
				OnRemoteNotificationReceived(rn);
			}
			NotificationServices.ClearRemoteNotifications();
		}
	}
	#endif




	#if UNITY_IPHONE

	#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
	public void RegisterForRemoteNotifications(RemoteNotificationType notificationTypes) {
	#else
	public void RegisterForRemoteNotifications(NotificationType notificationTypes) {;
	#endif



		#if (UNITY_IPHONE && !UNITY_EDITOR && PUSH_ENABLED) || SA_DEBUG_MODE

		string sysInfo = SystemInfo.operatingSystem;
		sysInfo = sysInfo.Replace("iPhone OS ", "");
		string[] chunks = sysInfo.Split('.');
		int majorVersion = int.Parse(chunks[0]);
		if (majorVersion >= 8) {
			_ISN_RegisterForRemoteNotifications((int) notificationTypes);
		} 

		#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
		NotificationServices.RegisterForRemoteNotificationTypes(notificationTypes);
		#else
		NotificationServices.RegisterForNotifications(notificationTypes);
		#endif

		DeviceTokenListener.Create ();

		#endif
	}
	#endif

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	public void RequestNotificationPermissions() {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_ISN_RequestNotificationPermissions ();
		#endif

	}
	

	public void ShowGmaeKitNotification (string title, string message) {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
		_ISN_ShowNotificationBanner (title, message);
		#endif
	}

	[System.Obsolete("ShowNotificationBanner is deprecated, please use ShowGmaeKitNotification instead.")]
	public void ShowNotificationBanner (string title, string message) {
		ShowGmaeKitNotification(title, message);
	}

	[System.Obsolete("CancelNotifications is deprecated, please use CancelAllLocalNotifications instead.")]
	public void CancelNotifications () {
		CancelAllLocalNotifications();
	}


	public void CancelAllLocalNotifications () {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_ISN_CancelNotifications();
		#endif
	}


	public void CancelLocalNotification (ISN_LocalNotification notification) {
		CancelLocalNotificationById(notification.Id);
	}


	public void CancelLocalNotificationById (int notificationId) {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_ISN_CancelNotificationById(notificationId.ToString());
		#endif
	}


	public void ScheduleNotification (ISN_LocalNotification notification) {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			int time =  System.Convert.ToInt32((notification.Date -DateTime.Now).TotalSeconds); 
			_ISN_ScheduleNotification (time, notification.Message, notification.UseSound, notification.Id.ToString(), notification.Badges, notification.Data);
		#endif
	}



	public void ApplicationIconBadgeNumber (int badges) {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_ISN_ApplicationIconBadgeNumber (badges);
		#endif

	}

	public void RequestNotificationSettings () {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
		_ISN_RequestNotificationSettings ();
		#endif
		
	}


	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------


	public static int GetNextNotificationId {
		get {
			int id = 1;
			if(UnityEngine.PlayerPrefs.HasKey(PP_ID_KEY)) {
				id = UnityEngine.PlayerPrefs.GetInt(PP_ID_KEY);
				id++;
			} 
			
			UnityEngine.PlayerPrefs.SetInt(PP_ID_KEY, id);
			return id;
		}
		
	}

	public static int AllowedNotificationsType {
		get {
			return _AllowedNotificationsType;
		}
	}

	public ISN_LocalNotification LaunchNotification {
		get {
			return _LaunchNotification;
		}
	}
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	public void OnDeviceTockeReceivedAction (IOSNotificationDeviceToken token) {
		OnDeviceTokenReceived(token);
	}

	private void OnNotificationScheduleResultAction (string array) {

		string[] data;
		data = array.Split("|" [0]);


		ISN_Result result = null;

		if(data[0].Equals("0")) {
			result =  new ISN_Result(false);
		} else {
			result =  new ISN_Result(true);
		}


		_AllowedNotificationsType = System.Convert.ToInt32(data[1]);

		OnNotificationScheduleResult(result);

	
	}

	private void OnNotificationSettingsInfoRetrived(string data) {
			int types = System.Convert.ToInt32(data);
			OnNotificationSettingsInfoResult(types);
	}

	private void OnLocalNotificationReceived_Event(string array) {
		string[] data;
		data = array.Split("|" [0]);

		string msg = data[0];
		int Id = System.Convert.ToInt32(data[1]);
		string notifDta = data[2];
		int badges = System.Convert.ToInt32(data[3]);

		ISN_LocalNotification n =  new ISN_LocalNotification(DateTime.Now, msg);
		n.SetData(notifDta);
		n.SetBadgesNumber(badges);
		n.SetId(Id);

		OnLocalNotificationReceived(n);
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------




	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
