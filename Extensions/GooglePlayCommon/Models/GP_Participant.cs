using UnityEngine;
using System.Collections;

public class GP_Participant  {

	private string _id;
	private string _playerid;
	private string _HiResImageUrl;
	private string _IconImageUrl;
	private string _DisplayName;

	private GP_ParticipantResult _result = null;



	private GP_RTM_ParticipantStatus _status = GP_RTM_ParticipantStatus.STATUS_UNRESPONSIVE;


	public GP_Participant(string uid, string playerUid, string stat, string hiResImg, string IconImg, string Name) {
		_id = uid;
		_playerid = playerUid;
		_status = (GP_RTM_ParticipantStatus) System.Convert.ToInt32(stat);
		_HiResImageUrl = hiResImg;
		_IconImageUrl = IconImg;
		_DisplayName = Name;
	}

	public void SetResult(GP_ParticipantResult r) {
		_result = r;
	}

	public string id {
		get {
			return _id;
		}
	}

	public string playerId {
		get {
			return _playerid;
		}
	}

	public string HiResImageUrl {
		get {
			return _HiResImageUrl;
		}
	}

	public string IconImageUrl {
		get {
			return _IconImageUrl;
		}
	}

	public string DisplayName {
		get {
			return _DisplayName;
		}
	}

	public GP_RTM_ParticipantStatus Status {
		get {
			return _status;
		}
	}

	public GP_ParticipantResult Result {
		get {
			return _result;
		}
	}
}
