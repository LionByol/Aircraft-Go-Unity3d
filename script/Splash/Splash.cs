using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

	void Awake()
	{
		//initilaize
		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		//initialize game
		R.selectedPlane = PlayerPrefs.GetInt("selected_aircraft", 0);
		R.gameWorld = PlayerPrefs.GetInt("selected_world", 0);			//sky world
		R.gameDifficulty = PlayerPrefs.GetInt("selected_difficulty", 0);	//game difficulty
		//make static instance
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnLoaded()
	{
		SceneManager.LoadScene ("menu");
	}

	public void LoadScene(string scene)
	{
		previousScene = SceneManager.GetActiveScene ().name;
		SceneManager.LoadScene (scene);
	}

	public void BackScene()
	{
		string tmp = previousScene;
		previousScene = SceneManager.GetActiveScene ().name;
		SceneManager.LoadScene (tmp);
	}

	static public Splash instance;
	public string previousScene;
	public string currentScene;
	public AudioClip backMusic1;
	public AudioClip backMusic2;
}
