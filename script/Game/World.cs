using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		//initialize
		R.gameWorld = PlayerPrefs.GetInt ("selected_world", 0);
		GetComponent<SpriteRenderer> ().sprite = worlds [R.gameWorld];

		//fade in
		gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 0);
		StartCoroutine (StartWorld ());

		//get screensize
		screenHeight = Camera.main.orthographicSize * 2.0f;
		screenWidth = screenHeight / Screen.height * Screen.width;

		//make clouds
		switch(R.gameWorld)
		{
		case 0:
			StartCoroutine (MakeClouds ());
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator StartWorld()
	{
		while (true)
		{
			yield return new WaitForSeconds (0.05f);
			float al = gameObject.GetComponent<SpriteRenderer> ().color.a;
			al += 0.05f;
			if (al > 1)
				break;
			gameObject.GetComponent<SpriteRenderer> ().color = new Color32((byte)255, (byte)255, (byte)255, (byte)(al*255));
		}
	}

	//make clouds
	IEnumerator MakeClouds()
	{
		while (true) {
			yield return new WaitForSeconds (1f);
			if (!GameController.instance.started)
				continue;
			GameObject cloud = (GameObject)GameObject.Instantiate (cloud_ori);
		}
	}

	static public World instance;
	public float screenWidth;
	public float screenHeight;
	public Sprite[] worlds;
	public GameObject cloud_ori;
}
