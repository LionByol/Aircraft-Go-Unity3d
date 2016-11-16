using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class ResourceS : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBack()
	{
		Splash.instance.BackScene();
	}
}
