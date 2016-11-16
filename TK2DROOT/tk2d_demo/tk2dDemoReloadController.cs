using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[AddComponentMenu("2D Toolkit/Demo/tk2dDemoReloadController")]
public class tk2dDemoReloadController : MonoBehaviour 
{
	void Reload()
	{
		SceneManager.LoadScene(Application.loadedLevel);
	}
}
