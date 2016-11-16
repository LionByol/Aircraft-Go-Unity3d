using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		R.gameWorld = PlayerPrefs.GetInt ("selected_world", 0);
		switch (R.gameWorld) {
		case 0:
			sky.GetComponent<UI2DSprite> ().color = Color.red;
			land.GetComponent<UI2DSprite> ().color = Color.white;
			sea.GetComponent<UI2DSprite> ().color = Color.white;
			ar.GetComponent<UI2DSprite> ().color = Color.white;
			break;
		case 1:
			sky.GetComponent<UI2DSprite> ().color = Color.white;
			land.GetComponent<UI2DSprite> ().color = Color.red;
			sea.GetComponent<UI2DSprite> ().color = Color.white;
			ar.GetComponent<UI2DSprite> ().color = Color.white;
			break;
		case 2:
			sky.GetComponent<UI2DSprite> ().color = Color.white;
			land.GetComponent<UI2DSprite> ().color = Color.white;
			sea.GetComponent<UI2DSprite> ().color = Color.red;
			ar.GetComponent<UI2DSprite> ().color = Color.white;
			break;
		case 3:
			sky.GetComponent<UI2DSprite> ().color = Color.white;
			land.GetComponent<UI2DSprite> ().color = Color.white;
			sea.GetComponent<UI2DSprite> ().color = Color.white;
			ar.GetComponent<UI2DSprite> ().color = Color.red;
			break;
		}

		switch (R.gameDifficulty) {
		case 0:
			siouxMark.SetActive (true);
			blackhawkMark.SetActive (false);
			kiowaMark.SetActive (false);
			break;
		case 1:
			siouxMark.SetActive (false);
			blackhawkMark.SetActive (true);
			kiowaMark.SetActive (false);
			break;
		case 2:
			siouxMark.SetActive (false);
			blackhawkMark.SetActive (false);
			kiowaMark.SetActive (true);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBack()
	{
		Splash.instance.BackScene ();
	}

	public void OnSkyWorld()
	{
		R.gameWorld = 0;
		PlayerPrefs.SetInt ("selected_world", R.gameWorld);
		sky.GetComponent<UIButton> ().defaultColor = Color.red;
		land.GetComponent<UIButton> ().defaultColor = Color.white;
		sea.GetComponent<UIButton> ().defaultColor = Color.white;
		ar.GetComponent<UIButton> ().defaultColor = Color.white;
	}
	public void OnLandWorld()
	{
		R.gameWorld = 2;
		PlayerPrefs.SetInt ("selected_world", R.gameWorld);
		sky.GetComponent<UIButton> ().defaultColor = Color.white;
		land.GetComponent<UIButton> ().defaultColor = Color.red;
		sea.GetComponent<UIButton> ().defaultColor = Color.white;
		ar.GetComponent<UIButton> ().defaultColor = Color.white;
	}
	public void OnSeaWorld()
	{
		R.gameWorld = 3;
		PlayerPrefs.SetInt ("selected_world", R.gameWorld);
		sky.GetComponent<UIButton> ().defaultColor = Color.white;
		land.GetComponent<UIButton> ().defaultColor = Color.white;
		sea.GetComponent<UIButton> ().defaultColor = Color.red;
		ar.GetComponent<UIButton> ().defaultColor = Color.white;
	}
	public void OnARWorld()
	{
		R.gameWorld = 4;
		PlayerPrefs.SetInt ("selected_world", R.gameWorld);
		sky.GetComponent<UIButton> ().defaultColor = Color.white;
		land.GetComponent<UIButton> ().defaultColor = Color.white;
		sea.GetComponent<UIButton> ().defaultColor = Color.white;
		ar.GetComponent<UIButton> ().defaultColor = Color.red;
	}

	public void OnSioux()
	{
		R.gameDifficulty = 0;
		PlayerPrefs.GetInt ("selected_difficulty", R.gameDifficulty);
		siouxMark.SetActive (true);
		blackhawkMark.SetActive (false);
		kiowaMark.SetActive (false);
	}
	public void OnBlackhawk()
	{
		R.gameDifficulty = 1;
		PlayerPrefs.GetInt ("selected_difficulty", R.gameDifficulty);
		siouxMark.SetActive (false);
		blackhawkMark.SetActive (true);
		kiowaMark.SetActive (false);
	}
	public void OnKiowa()
	{
		R.gameDifficulty = 2;
		PlayerPrefs.GetInt ("selected_difficulty", R.gameDifficulty);
		siouxMark.SetActive (false);
		blackhawkMark.SetActive (false);
		kiowaMark.SetActive (true);
	}

	public GameObject siouxMark;
	public GameObject blackhawkMark;
	public GameObject kiowaMark;

	public GameObject sky;
	public GameObject land;
	public GameObject sea;
	public GameObject ar;
}
