using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		//initialize

		//change Music
		Splash.instance.gameObject.GetComponent<AudioSource>().clip = Splash.instance.backMusic2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static public GameController instance;

	public bool started;		//game started
}
