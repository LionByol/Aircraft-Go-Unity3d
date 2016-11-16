////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using UnityEngine;
using System.Collections;

public class GK_Score  {
	

	private int _rank;
	private string _score;

	private string _playerId;
	private string _leaderboardId;

	private GK_CollectionType _collection;
	private GK_TimeSpan _timeSpan;


	public GK_Score(string vScore, int vRank, GK_TimeSpan vTimeSpan, GK_CollectionType sCollection, string lid, string pid) {
		_score = vScore;
		_rank = vRank;
		
		_playerId = pid;
		_leaderboardId = lid;
		
		
		_timeSpan  = vTimeSpan;
		_collection = sCollection;
		
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	
	public double GetDoubleScore() {
		return GetLongScore () / 100f;
	}
	
	public long GetLongScore() {
		return System.Convert.ToInt64 (_score);
	}
	
	
	public int GetRank() {
		return rank;
	}


	//--------------------------------------
	// GET / SET
	//--------------------------------------

	public int rank {
		get {
			return _rank;
		}
	}

	public string score {
		get {
			return _score;
		}
	}
	
	public string playerId {
		get {
			return _playerId;
		}
	}
	
	public string leaderboardId {
		get {
			return _leaderboardId;
		}
	}
	
	
	public GK_CollectionType collection {
		get {
			return _collection;
		}
	}
	
	
	public GK_TimeSpan timeSpan {
		get {
			return _timeSpan;
		}
	}

}

