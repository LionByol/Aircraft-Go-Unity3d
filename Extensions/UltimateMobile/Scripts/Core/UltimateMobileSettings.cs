using UnityEngine;
using System.IO;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
[InitializeOnLoad]
#endif

public class UltimateMobileSettings : ScriptableObject {

	public const string VERSION_NUMBER = "3.8";
	
	private const string UMSettingsAssetName = "UltimateMobileSettings";
	private const string UMSettingsPath = "Extensions/UltimateMobile/Resources";
	private const string UMSettingsAssetExtension = ".asset";




	public UM_IOSAdEngineOprions IOSAdEdngine = UM_IOSAdEngineOprions.iAdAppNetwork;
	public UM_WP8AdEngineOprions WP8AdEdngine = UM_WP8AdEngineOprions.GoogleMobileAd;


	public bool IsInAppSettingsOpen = false;
	public bool IsInAppSettingsPlatfromsOpen = true;
	public bool IsInAppSettingsProductsOpen = true;
	public bool IsCameraAndGallerySettingsOpen = true;
	public bool IsCameraAndGalleryAndroidSettingsOpen = false;
	public bool IsCameraAndGalleryIOSSettingsOpen = false;



	public bool IsLPSettingsOpen = true;
	public bool IsLP_Android_SettingsOpen = false;
	public bool IsLP_IOS_SettingsOpen = false;
	public bool ThirdPartyParams_SettingsOpen = false;


	[SerializeField]
	public List<UM_Leaderboard> Leaderboards =  new List<UM_Leaderboard>();
	[SerializeField]
	public List<UM_Achievement> Achievements =  new List<UM_Achievement>();



	public bool IsaAdvertisementSettingsOpen = true;	
	public bool AdIOSSettings = false;
	public bool AdAndroidSettings = false;	
	public bool AdWp8Settings = false;	


	public bool IsGameServiceOpen = true;
	public bool IsLeaderBoardsOpen = false;
	public bool IsAchievementsOpen = false;
	public bool IsMoreActionsOpen = true;
	public bool IsMoreSettingsOpen = true;



	[SerializeField]
	public List<UM_InAppProduct> InAppProducts =  new List<UM_InAppProduct>();
	
	private static UltimateMobileSettings instance = null;
	
	
	public static UltimateMobileSettings Instance {
		
		get {
			if (instance == null) {
				instance = Resources.Load(UMSettingsAssetName) as UltimateMobileSettings;
				
				if (instance == null) {


					
					// If not found, autocreate the asset object.
					instance = CreateInstance<UltimateMobileSettings>();
					#if UNITY_EDITOR

					FileStaticAPI.CreateFolder(UMSettingsPath);
					
					string fullPath = Path.Combine(Path.Combine("Assets", UMSettingsPath),
					                               UMSettingsAssetName + UMSettingsAssetExtension
					                               );
					
					AssetDatabase.CreateAsset(instance, fullPath);
					#endif
				}
			}
			return instance;
		}
	}

	//--------------------------------------
	// IN Apps
	//--------------------------------------

	public void AddProduct(UM_InAppProduct p) {
		InAppProducts.Add(p);
	}

	public void RemoveProduct(UM_InAppProduct p) {
		InAppProducts.Remove(p);
	}


	public UM_InAppProduct GetProductById(string id) {
		foreach(UM_InAppProduct p in InAppProducts) {
			if(p.id.Equals(id)) {
				return p;
			}
		}

		return null;
	}


	public UM_InAppProduct GetProductByIOSId(string id) {
		foreach(UM_InAppProduct p in InAppProducts) {
			if(p.IOSId.Equals(id)) {
				return p;
			}
		}
		
		return null;
	}


	public UM_InAppProduct GetProductByAndroidId(string id) {
		foreach(UM_InAppProduct p in InAppProducts) {
			if(p.AndroidId.Equals(id)) {
				return p;
			}
		}
		
		return null;
	}

	public UM_InAppProduct GetProductByWp8Id(string id) {
		foreach(UM_InAppProduct p in InAppProducts) {
			if(p.WP8Id.Equals(id)) {
				return p;
			}
		}
		
		return null;
	}


	//--------------------------------------
	// Achievements
	//--------------------------------------


	public void AddAchievement(UM_Achievement a) {
		Achievements.Add(a);
	}
	
	public void RemoveAchievement(UM_Achievement a) {
		Achievements.Remove(a);
	}


	public UM_Achievement GetAchievementById(string id) {
		foreach(UM_Achievement a in Achievements) {
			if(a.id.Equals(id)) {
				return a;
			}
		}
		
		return null;
	}
	
	
	public UM_Achievement GetAchievementByIOSId(string id) {
		foreach(UM_Achievement a in Achievements) {
			if(a.IOSId.Equals(id)) {
				return a;
			}
		}
		
		return null;
	}
	
	
	public UM_Achievement GetAchievementByAndroidId(string id) {
		foreach(UM_Achievement a in Achievements) {
			if(a.AndroidId.Equals(id)) {
				return a;
			}
		}
		
		return null;
	}


	
	//--------------------------------------
	// Leaderboards
	//--------------------------------------


	
	public void AddLeaderboard(UM_Leaderboard l) {
		Leaderboards.Add(l);
	}
	
	public void RemoveLeaderboard(UM_Leaderboard l) {
		Leaderboards.Remove(l);
	}
	
	
	public UM_Leaderboard GetLeaderboardById(string id) {
		foreach(UM_Leaderboard l in Leaderboards) {
			if(l.id.Equals(id)) {
				return l;
			}
		}
		
		return null;
	}
	
	
	public UM_Leaderboard GetLeaderboardByIOSId(string id) {
		foreach(UM_Leaderboard l in Leaderboards) {
			if(l.IOSId.Equals(id)) {
				return l;
			}
		}
		return null;
	}
	
	
	public UM_Leaderboard GetLeaderboardByAndroidId(string id) {
		foreach(UM_Leaderboard l in Leaderboards) {
			if(l.AndroidId.Equals(id)) {
				return l;
			}
		}
		
		return null;
	}

}
