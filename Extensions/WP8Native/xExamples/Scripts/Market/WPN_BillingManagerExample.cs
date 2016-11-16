using UnityEngine;
using System.Collections;

public class WPN_BillingManagerExample : MonoBehaviour {


	public const string YOUR_DURABLE_PRODUCT_ID_CONSTANT 		= "item2";
	public const string YOUR_CONSUMABLE_PRODUCT_ID_CONSTANT		= "item1";



	private static bool _IsInited = false;



	public static void Init() {

		WP8InAppPurchasesManager.OnInitComplete += HandleOnInitComplete;
		WP8InAppPurchasesManager.OnPurchaseFinished += HandleOnPurchaseFinished;
	

		WP8InAppPurchasesManager.instance.init();
	}


	public static void Purchase(string productId) {
		WP8InAppPurchasesManager.instance.purchase(productId);
	}


	public static bool IsInited {
		get {
			return _IsInited;
		}
	}



	private static void HandleOnPurchaseFinished(WP8PurchseResponce responce) {
	
		if(responce.IsSuccses) {
			//Unlock logic for product with id recponce.productId should be here
			WP8Dialog.Create("Purchase Succse", "Product: " + responce.productId);
		} else {
			//Purchase fail logic for product with id recponce.productId should be here
			WP8Dialog.Create("Purchase Failed", "Product: " + responce.productId);
		}
	}


	private static void HandleOnInitComplete(WP8_InAppsInitResult result) {

		if(result.IsFailed) {
			return;
		}


		_IsInited = true;

		WP8InAppPurchasesManager.OnInitComplete -= HandleOnInitComplete;

		foreach(WP8ProductTemplate product in WP8InAppPurchasesManager.instance.products) {
			if(product.Type == WP8PurchaseProductType.Durable) {
				if(product.isPurchased) {
					//The Durable product was purchased, we should check here 
					//if the content is unlocked for our Durable product.

					Debug.Log("Product " + product.Name + " is purchased");

				}
			}
		}

		WP8Dialog.Create("market Initted", "Total products avaliable: " + WP8InAppPurchasesManager.instance.products.Count);
	}




}

