using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WP8_InAppsInitResult : WP8_Result {

	public WP8_InAppsInitResult():base(true) {

	}


	public List<WP8ProductTemplate> products {
		get {
			return WP8InAppPurchasesManager.instance.products;
		}
	}
}
