using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WP8InAppPurchasesManager {
	
	private static WP8InAppPurchasesManager _instance = null;

	private bool _IsInitialized = false;
	
	private List<WP8ProductTemplate> _products =  new List<WP8ProductTemplate>();
		
	public const string  INITIALIZED = "PRODUCTS_DETAILS_LOADED";
	public const string  PRODUCT_PURCHASE_FINISHED = "PRODUCT_PURCHASE_FINISHED";


	public static event Action<WP8_InAppsInitResult> OnInitComplete   = delegate {};
	public static event Action<WP8PurchseResponce> OnPurchaseFinished = delegate {};

	private List<WP8PurchseResponce> _defferedPurchases = new List<WP8PurchseResponce>();


	public static WP8InAppPurchasesManager instance {
		get {
			if(_instance == null) {
				_instance =  new WP8InAppPurchasesManager();
			}
			
			return _instance;
		}
	}
	
	public void init() {
		#if UNITY_WP8 || UNITY_METRO
		WP8Native.InAppPurchases.productsInit(ProductsDetailsDelegate, DefferedProductPurchseDelegate);
		#endif
	}
	
	public void purchase(string productId) {
		#if UNITY_WP8 || UNITY_METRO
		WP8Native.InAppPurchases.BuyItem(productId, ProductPurchseDelegate);
		#endif
	}
		
	public List<WP8ProductTemplate> products  {
		get {
			return _products;
		}
	}

	public bool IsInitialized {
		get {
			return _IsInitialized;
		}
	}

	public WP8ProductTemplate GetProductById(string id) {
		foreach(WP8ProductTemplate p in _products) {
			if(p.ProductId.Equals(id)) {
				return p;
			}
		}

		return null;
	} 
		
	private void ProductsDetailsDelegate(string data) {
				
		WP8_InAppsInitResult result;

		if(data.Equals(string.Empty)) {
			Debug.Log("InAppPurchaseManager, you have no avaiable products");
			result = new WP8_InAppsInitResult();
			OnInitComplete(result);
			return;
		}
		
		string[] storeData;
		storeData = data.Split("|" [0]);
		
		for ( int i = 0; i < storeData.Length; i += 7 ) {
			WP8ProductTemplate tpl =  new WP8ProductTemplate();
			tpl.ImgURL = storeData[i];
			tpl.Name = storeData[i + 1];
			tpl.ProductId = storeData[i + 2];
			tpl.Price = storeData[i + 3];
            if (storeData[i+4].Trim().Length > 0)
			    tpl.Type = (WP8PurchaseProductType)Enum.Parse(typeof(WP8PurchaseProductType), storeData[i + 4]);
			tpl.Description = storeData[i + 5];
			tpl.isPurchased = (Boolean)Boolean.Parse(storeData[i + 6]);
			
			_products.Add(tpl);
			
		}

		_IsInitialized = true;
		result = new WP8_InAppsInitResult();

		OnInitComplete(result);

		//Deffered purchases event dispatch
		foreach (WP8PurchseResponce defferedPurchase in _defferedPurchases) {
			OnPurchaseFinished(defferedPurchase);
		}
		_defferedPurchases.Clear();
	}
	
	private void ProductPurchseDelegate(string data) {
		
		WP8PurchseResponce recponce = GetPurchaseResponseFromString(data);

		OnPurchaseFinished(recponce);
	}

	private void DefferedProductPurchseDelegate(string data) {
		WP8PurchseResponce defferdResponse = GetPurchaseResponseFromString(data);
		_defferedPurchases.Add(defferdResponse);
	}

	private WP8PurchseResponce GetPurchaseResponseFromString(string data) {
		string[] storeData;
		storeData = data.Split("|" [0]);
		
		WP8PurchaseCodes code = (WP8PurchaseCodes)Enum.Parse(typeof(WP8PurchaseCodes), storeData[0]);
		string info_str = storeData[1];
		string productID = storeData[2];
		
		if ( code == WP8PurchaseCodes.SUCCSES ) {
			foreach ( WP8ProductTemplate product in _products) {
				if ( product.ProductId == productID && product.Type == WP8PurchaseProductType.Durable ) {
					product.isPurchased = true;
				}
			}
		}
		
		WP8PurchseResponce recponce =  new WP8PurchseResponce(code, info_str);
		recponce.productId = productID;

		return recponce;
	}

}
