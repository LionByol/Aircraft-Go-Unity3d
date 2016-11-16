using UnityEngine;
using System.Collections;

public class MissionSelect : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		//get available levels
		R.availableLevel = new int[21];
		for (int i = 1; i < 21; i++) {
			if (i == 0) {
				R.availableLevel[i] = PlayerPrefs.GetInt ("available_level" + i, 0);
			} else {
				R.availableLevel[i] = PlayerPrefs.GetInt ("available_level" + i, -1);
			}
		}

		UpdateLevels ();
	}
	
	// Update is called once per frame
	void Update () {
		//refresh scrollview
		grid.Reposition ();
		//change scroll view
		if (scrollDirection == -1) {
			float tmp = levelScroll.value + 0.08f;
			if (tmp > 1)
				tmp = 1;
			levelScroll.value = tmp;
		} else if (scrollDirection == 1) {
			float tmp = levelScroll.value - 0.08f;
			if (tmp < 0)
				tmp = 0;
			levelScroll.value = tmp;
		}
	}

	void UpdateLevels()
	{
		for (int i = 0; i < 21; i++) {
			GameObject label = levels [i].transform.FindChild ("Label").gameObject;
			GameObject locK = levels [i].transform.FindChild ("lock").gameObject;
			UI2DSprite star1 = levels [i].transform.FindChild ("star1").gameObject.GetComponent<UI2DSprite>();
			UI2DSprite star2 = levels [i].transform.FindChild ("star2").gameObject.GetComponent<UI2DSprite>();
			UI2DSprite star3 = levels [i].transform.FindChild ("star3").gameObject.GetComponent<UI2DSprite>();
			if (R.availableLevel[i] == -1) {
				label.SetActive (false);
				levels [i].GetComponent<UIButton> ().enabled = false;
				star1.sprite2D = failStarSprite;
				star2.sprite2D = failStarSprite;
				star3.sprite2D = failStarSprite;
			} else {
				locK.SetActive (false);
				levels [i].GetComponent<UIButton> ().enabled = true;
				if (R.availableLevel[i] == 0) {
					star1.sprite2D = failStarSprite;
					star2.sprite2D = failStarSprite;
					star3.sprite2D = failStarSprite;
				} else if (R.availableLevel [i] == 1) {
					star1.sprite2D = winStarSprite;
					star2.sprite2D = failStarSprite;
					star3.sprite2D = failStarSprite;
				} else if (R.availableLevel [i] == 2) {
					star1.sprite2D = winStarSprite;
					star2.sprite2D = winStarSprite;
					star3.sprite2D = failStarSprite;
				} else if (R.availableLevel [i] == 3) {
					star1.sprite2D = winStarSprite;
					star2.sprite2D = winStarSprite;
					star3.sprite2D = winStarSprite;
				}
			}
		}
	}

	public void OnUp()
	{
		scrollDirection = -1;
	}

	public void OnDown()
	{
		scrollDirection = 1;
	}

	public void OnBack()
	{
		Splash.instance.BackScene ();
	}

	public void OnScore()
	{
		
	}

	public void OnShare()
	{
		
	}

	int scrollDirection;

	public UIScrollBar levelScroll;
	public UIGrid grid;
	public GameObject[] levels;
	public Sprite winStarSprite;
	public Sprite failStarSprite;

	public void OnLevel1()
	{
		R.selectedLevel = 0;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel2()
	{
		R.selectedLevel = 1;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel3()
	{
		R.selectedLevel = 2;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel4()
	{
		R.selectedLevel = 3;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel5()
	{
		R.selectedLevel = 4;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel6()
	{
		R.selectedLevel = 5;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel7()
	{
		R.selectedLevel = 6;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel8()
	{
		R.selectedLevel = 7;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel9()
	{
		R.selectedLevel = 8;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel10()
	{
		R.selectedLevel = 9;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel11()
	{
		R.selectedLevel = 10;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel12()
	{
		R.selectedLevel = 11;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel13()
	{
		R.selectedLevel = 12;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel14()
	{
		R.selectedLevel = 13;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel15()
	{
		R.selectedLevel = 14;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel16()
	{
		R.selectedLevel = 15;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel17()
	{
		R.selectedLevel = 16;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel18()
	{
		R.selectedLevel = 17;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel19()
	{
		R.selectedLevel = 18;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel20()
	{
		R.selectedLevel = 19;
		Splash.instance.LoadScene ("rescuemission");
	}
	public void OnLevel21()
	{
		R.selectedLevel = 20;
		Splash.instance.LoadScene ("rescuemission");
	}
}
