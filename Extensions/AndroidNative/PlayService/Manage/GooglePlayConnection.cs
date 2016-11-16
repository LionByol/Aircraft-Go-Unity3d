////////////////////////////////////////////////////////////////////////////////
//  
// @module Android Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;


public class GooglePlayConnection : SA_Singleton<GooglePlayConnection> {
	
	private bool _isInitialized = false;
	

	//Actions
	public static event Action<GooglePlayConnectionResult> ActionConnectionResultReceived =  delegate {};

	public static event Action<GPConnectionState> ActionConnectionStateChanged =  delegate {};
	public static event Action ActionPlayerConnected =  delegate {};
	public static event Action ActionPlayerDisconnected =  delegate {};




	private static GPConnectionState _state = GPConnectionState.STATE_UNCONFIGURED;


	//--------------------------------------
	// INITIALIZE
	//--------------------------------------


	void Awake() {
		DontDestroyOnLoad(gameObject);

		GooglePlayManager.instance.Create();
		init();
	}


	//--------------------------------------
	// PUBLIC API CALL METHODS
	//--------------------------------------

	private void init() {
		string connectionString = "";
		if(AndroidNativeSettings.Instance.EnableGamesAPI) {
			connectionString += "GamesAPI";
		}

		if(AndroidNativeSettings.Instance.EnableAppStateAPI) {
			connectionString += "AppStateAPI";
		}

		if(AndroidNativeSettings.Instance.EnablePlusAPI) {
			connectionString += "PlusAPI";
		}

		if(AndroidNativeSettings.Instance.EnableDriveAPI) {
			connectionString += "DriveAPI";
		}

		AN_GMSGeneralProxy.setConnectionParams (AndroidNativeSettings.Instance.ShowConnectingPopup);
		AN_GMSGeneralProxy.playServiceInit(connectionString);
	}

	public void connect()  {
		connect(null);
	}



	public void connect(string accountName) {

		if(_state == GPConnectionState.STATE_CONNECTED || _state == GPConnectionState.STATE_CONNECTING) {
			return;
		}

		OnStateChange(GPConnectionState.STATE_CONNECTING);

		if(accountName != null) {
			AN_GMSGeneralProxy.playServiceConnect (accountName);
		} else {
			AN_GMSGeneralProxy.playServiceConnect ();
		}

	}

	public void disconnect() {

		if(_state == GPConnectionState.STATE_DISCONNECTED || _state == GPConnectionState.STATE_CONNECTING) {
			return;
		}

		OnStateChange(GPConnectionState.STATE_DISCONNECTED);
		AN_GMSGeneralProxy.playServiceDisconnect ();

	}


	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------


	public static bool CheckState() {
		switch(_state) {
			case GPConnectionState.STATE_CONNECTED:
			return true;
			case GPConnectionState.STATE_DISCONNECTED:
			instance.connect ();
			return false;
			default:
			return false;
		}
	}



	//--------------------------------------
	// GET / SET
	//--------------------------------------

	public static GPConnectionState state {
		get {
			return _state;
		}
	}


	public  bool isInitialized {
		get {
			return _isInitialized;
		}
	}




	//--------------------------------------
	// EVENTS
	//--------------------------------------

	void OnApplicationPause(bool pauseStatus) {
		AN_GMSGeneralProxy.OnApplicationPause(pauseStatus);
	}
	

	private void OnPlayServiceDisconnected(string data) {
		OnStateChange(GPConnectionState.STATE_DISCONNECTED);
	}


	private void OnConnectionResult(string data) {
		string[] res;
		res = data.Split(AndroidNative.DATA_SPLITTER [0]);
		GooglePlayConnectionResult result = new GooglePlayConnectionResult();
		result.code = (GP_ConnectionResultCode) System.Convert.ToInt32(res[0]);



		if(System.Convert.ToInt32(res[1]) == 1) {
			result.HasResolution = true;
		} else {
			result.HasResolution = false;
		}


		if(result.IsSuccess) {
			OnStateChange(GPConnectionState.STATE_CONNECTED);
		} else {
			if(!result.HasResolution) {
				OnStateChange(GPConnectionState.STATE_DISCONNECTED);
			}
		}

		ActionConnectionResultReceived(result);

	}


	private void OnStateChange(GPConnectionState connectionState) {

		_state = connectionState;
		switch(_state) {
			case GPConnectionState.STATE_CONNECTED:
				ActionPlayerConnected();
				break;
			case GPConnectionState.STATE_DISCONNECTED:
				ActionPlayerDisconnected();
				break; 
		}

		ActionConnectionStateChanged(_state);

		Debug.Log("Play Serice Connection State -> " + _state.ToString());
	}

	


}
