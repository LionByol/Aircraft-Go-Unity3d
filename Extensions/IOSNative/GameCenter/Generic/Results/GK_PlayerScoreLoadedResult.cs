using UnityEngine;
using System.Collections;

public class GK_PlayerScoreLoadedResult : ISN_Result {

	
	private GK_Score _score = null;


	public GK_PlayerScoreLoadedResult():base(false) {

	}

	public GK_PlayerScoreLoadedResult(string errorData):base(errorData) {
		
	}

	public GK_PlayerScoreLoadedResult(GK_Score score):base(true) {
		_score = score;
	}
	
	
	public GK_Score loadedScore {
		get {
			return _score;
		}
	}
}
