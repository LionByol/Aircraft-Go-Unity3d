////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WPM_MarketExample : WPNFeaturePreview {

	

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------


	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------


	void OnGUI() {
		
		UpdateToStartPos();
		
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40), "Market Example", style);
		StartY+= YLableStep;
		
		
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Init")) {
			WPN_BillingManagerExample.Init();
		}

		if(!WPN_BillingManagerExample.IsInited) {
			return;
		}
		
		
		StartX += XButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Buy Consumable")) {
			WPN_BillingManagerExample.Purchase(WPN_BillingManagerExample.YOUR_CONSUMABLE_PRODUCT_ID_CONSTANT);
		}
		
		
		StartX += XButtonStep;
		if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Buy Durable")) {
			WPN_BillingManagerExample.Purchase(WPN_BillingManagerExample.YOUR_DURABLE_PRODUCT_ID_CONSTANT);
		}
		
		
	}
	

	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}