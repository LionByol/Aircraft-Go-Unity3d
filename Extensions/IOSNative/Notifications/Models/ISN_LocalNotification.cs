using UnityEngine;
using System.Collections;
using System;

public class ISN_LocalNotification  {


	private int _Id = 0;
	private DateTime _Date;
	private string _Message = string.Empty;
	private bool _UseSound = true;
	private int _Badges = 0;
	private string _Data = string.Empty;


	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	public ISN_LocalNotification(DateTime time, string message, bool useSound = true) {


		_Id = IOSNotificationController.GetNextNotificationId;
		_Date = time;
		_Message = message;
		_UseSound = useSound;

	}


	//--------------------------------------
	// Internal use only
	//--------------------------------------

	public void SetId(int id) {
		_Id = id;
	}

	//--------------------------------------
	// Public Methods
	//--------------------------------------

	public void SetData(string data) {
		_Data = data;
	}


	public void SetBadgesNumber(int badges) {
		_Badges = badges;
	}

	public void Schedule() {
		IOSNotificationController.Instance.ScheduleNotification(this);
	}


	//--------------------------------------
	// Get / Set
	//--------------------------------------


	public int Id {
		get {
			return _Id;
		}
	}

	public DateTime Date {
		get {
			return _Date;
		}
	}

	public string Message {
		get {
			return _Message;
		}
	}

	public bool UseSound {
		get {
			return _UseSound;
		}
	}

	public int Badges {
		get {
			return _Badges;
		}
	}

	public string Data {
		get {
			return _Data;
		}
	}
}
