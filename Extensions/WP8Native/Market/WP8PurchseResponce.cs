using UnityEngine;
using System.Collections;

public class WP8PurchseResponce  {

	private WP8PurchaseCodes _code;
	
	private string _error;
	private string _receipt;

	public string productId;
		
	public WP8PurchseResponce(WP8PurchaseCodes c, string val) {
		_code = c;
		if(IsSuccses) {
			_receipt = val;
		} else {
			_error = val;
		}
		
	}
		
	public bool IsSuccses {
		get {
			if(_code == WP8PurchaseCodes.SUCCSES) {
				return true;
			} else {
				return false;
			}
		}
	}

	public string receipt {
		get {
			return _receipt;
		}
	}

	public string error {
		get {
			return _error;
		}
	}
}
