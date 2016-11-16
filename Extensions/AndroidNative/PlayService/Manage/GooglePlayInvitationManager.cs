using UnityEngine;
using System;
using System.Collections.Generic;

public class GooglePlayInvitationManager : SA_Singleton<GooglePlayInvitationManager> {


	public static event Action<GP_Invite> ActionInvitationReceived = delegate{};
	public static event Action<GP_Invite> ActionInvitationAccepted = delegate{};

	public static event Action<AN_InvitationInboxCloseResult> ActionInvitationInboxClosed =  delegate{};
	public static event Action<string> ActionInvitationRemoved = delegate {};

	void Awake() {
		DontDestroyOnLoad(gameObject);
		Debug.Log("GooglePlayInvitationManager Created");
	}


	public void Init() {
		//Empty init. Inv manager will be inited if TBM or RTM controlles was created
	}



	private void OnInvitationReceived(string data) {		
		string[] storeData = data.Split(AndroidNative.DATA_SPLITTER [0]);
		ActionInvitationReceived(InviteFromString(storeData, 0));
	}

	private void OnInvitationAccepted(string data) {		
		string[] storeData = data.Split(AndroidNative.DATA_SPLITTER [0]);
		ActionInvitationAccepted(InviteFromString(storeData, 0));


		Debug.Log("OnInvitationAccepted+++");
	}

	private void OnInvitationRemoved(string invId) {		
		ActionInvitationRemoved (invId);
	}

	private void OnInvitationBoxUiClosed(string response) {
		AN_InvitationInboxCloseResult result =  new AN_InvitationInboxCloseResult(response);
		ActionInvitationInboxClosed(result);
	}

	private GP_Invite InviteFromString(string[] storeData, int offset){
		GP_Invite inv =  new GP_Invite();
		inv.Id = storeData[0 + offset];
		inv.CreationTimestamp = System.Convert.ToInt64 (storeData[1 + offset]);
		inv.InvitationType = (GP_InvitationType)System.Convert.ToInt32 (storeData[2 + offset]);
		inv.Variant = System.Convert.ToInt32 (storeData [3 + offset]);
		inv.Participant = GooglePlayManager.ParseParticipanData (storeData, 4 + offset);

		return inv;
	}



	public void RegisterInvitationListener() {
		AN_GMSInvitationProxy.registerInvitationListener ();
	}

	public void UnregisterInvitationListener() {
		AN_GMSInvitationProxy.unregisterInvitationListener ();
	}
}
