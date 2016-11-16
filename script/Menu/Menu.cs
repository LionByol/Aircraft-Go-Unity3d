using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.InteropServices;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//get screenshot
		StartCoroutine (getScreenshot ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPlayNow()
	{
		R.gameMode = 0;			//play mode
		Splash.instance.LoadScene ("game");
	}

	public void OnRescueMission()
	{
		Splash.instance.LoadScene ("missionselect");
	}

	public void OnChangeAircraft()
	{
		Splash.instance.LoadScene ("changeaircraft");
	}

	public void OnXP()
	{
		
	}

	public void OnDallar()
	{
		Splash.instance.LoadScene ("shop");
	}

	public void OnGameController()
	{
		Splash.instance.LoadScene ("gamemode");
	}

	public void OnAchievement()
	{
		Splash.instance.LoadScene ("achievement");
	}

	public void OnFB()
	{
		
	}

	public void OnScore()
	{
		Splash.instance.LoadScene ("highscore");
	}

	public void OnShare()
	{
		string applink = "http://google.com/";
		string details = "PopUp Game";

		string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".jpg");
		Debug.Log(destination);
		File.WriteAllBytes(destination, screenshot);
		#if UNITY_ANDROID
		if(!Application.isEditor)
		{
			string gameLink = "Download the game on play store at "+"\n"+applink;
			string subject = "Popup Game";

			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), details +"\n"+ gameLink);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);

			AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", destination);// Set Image Path Here
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
			bool fileExist = fileObject.Call<bool>("exists");
			Debug.Log("File exist : " + fileExist);
			if (fileExist)
				intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			currentActivity.Call("startActivity", intentObject);
		}
		#elif UNITY_IOS
			string path = Application.persistentDataPath + "/MyImage.png";
			File.WriteAllBytes (path, screenshot);
			string path_ = "MyImage.png";
			GeneralSharingiOSBridge.ShareTextWithImage(path, "This is crazy game. That's great!");
		#endif
	}

	//--- get screenshot
	IEnumerator getScreenshot()
	{
		yield return new WaitForEndOfFrame();

		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		screenshot = tex.EncodeToJPG();
	}

	public void OnMusic()
	{
		Splash.instance.gameObject.GetComponent<AudioSource> ().mute = !Splash.instance.gameObject.GetComponent<AudioSource> ().mute;
	}

	byte[] screenshot;
}
