using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GhangeAircraft : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		//get available planes
		R.availablePlane = new int[10];
		R.availablePlane[0] = 1;
		for (int i = 1; i < 10; i++) {
			R.availablePlane[i] = PlayerPrefs.GetInt ("available_plane" + i, 0);
		}
		ChangeButtons();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnBack()
	{
		Splash.instance.LoadScene ("menu");
	}

	public void OnDallar()
	{
		Splash.instance.LoadScene ("shop");
	}

	public void OnGamecontroller()
	{
		Splash.instance.LoadScene ("gamemode");
	}

	void ChangeButtons()
	{
		for (int i = 0; i < 10; i++)
		{
			//set selected/unselected planes
			if (i == R.selectedPlane) {
				planes [i].GetComponent<UIButton> ().normalSprite2D = selectedSprite;
			} else {
				planes [i].GetComponent<UIButton> ().normalSprite2D = normalSprite;
			}

			//set unavailable/available planes
			if (R.availablePlane [i] == 0) {
				planes [i].GetComponent<BoxCollider> ().enabled = false;
				planes [i].GetComponent<UI2DSprite> ().color = new Color32 (127, 127, 127, 255);
			} else {
				planes [i].GetComponent<BoxCollider> ().enabled = true;
				planes [i].GetComponent<UI2DSprite> ().color = new Color32 (255, 255, 255, 255);
			}
		}
	}

	public void OnPlane1()
	{
		PlayerPrefs.SetInt ("selected_aircraft", 0);
		R.selectedPlane = 0;
		ChangeButtons ();
	}

	public void OnPlane2()
	{
		PlayerPrefs.SetInt ("selected_aircraft", 1);
		R.selectedPlane = 1;
		ChangeButtons ();
	}

	public void OnPlane3()
	{
		PlayerPrefs.SetInt ("selected_aircraft", 2);
		R.selectedPlane = 2;
		ChangeButtons ();
	}

	public void OnPlane4()
	{
		PlayerPrefs.SetInt ("selected_aircraft", 3);
		R.selectedPlane = 3;
		ChangeButtons ();
	}

	public void OnPlane5()
	{
		PlayerPrefs.SetInt ("selected_aircraft", 4);
		R.selectedPlane = 4;
		ChangeButtons ();
	}

	public void OnPlane6()
	{
		PlayerPrefs.SetInt ("selected_aircraft", 5);
		R.selectedPlane = 5;
		ChangeButtons ();
	}

	public void OnPlane7()
	{
		PlayerPrefs.SetInt ("selected_aircraft", 6);
		R.selectedPlane = 6;
		ChangeButtons ();
	}

	public void OnPlane8()
	{
		PlayerPrefs.SetInt ("selected_aircraft", 7);
		R.selectedPlane = 7;
		ChangeButtons ();
	}

	public void OnPlane9()
	{
		PlayerPrefs.SetInt ("selected_aircraft", 8);
		R.selectedPlane = 8;
		ChangeButtons ();
	}

	public void OnPlane10()
	{
		PlayerPrefs.SetInt ("selected_aircraft", 9);
		R.selectedPlane = 9;
		ChangeButtons ();
	}

	public GameObject[] planes;
	public Sprite selectedSprite;
	public Sprite normalSprite;
}
