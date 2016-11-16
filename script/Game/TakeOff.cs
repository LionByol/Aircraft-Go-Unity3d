using UnityEngine;
using System.Collections;

public class TakeOff : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		//initialize
		GameController.instance.started = false;

		//make take off aircraft
		GameObject newPlane = (GameObject)GameObject.Instantiate (plane[R.selectedPlane]);
		newPlane.transform.parent = transform;
		newPlane.transform.localPosition = new Vector3 (0, 0, 0);
		GetComponent<Animator> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnEndTakeoff()
	{
		StartCoroutine (DestroyTakeoff());
	}

	IEnumerator DestroyTakeoff()
	{
		while (true)
		{
			yield return new WaitForSeconds (0.05f);
			float al = transform.parent.gameObject.GetComponent<SpriteRenderer> ().color.a;
			al -= 0.05f;
			transform.parent.gameObject.GetComponent<SpriteRenderer> ().color = new Color32((byte)255, (byte)255, (byte)255, (byte)(al*255));
			if (al < 0)
				break;
		}
		GameController.instance.started = true;

		world.SetActive (true);

		GameObject realPlane = (GameObject)GameObject.Instantiate (plane [R.selectedPlane]);
		realPlane.transform.position = new Vector3 (0, 0, -2);

		touch.SetActive (true);

		Destroy (gameObject.transform.parent.gameObject);
	}

	public GameObject[] plane;
	public GameObject world;
	public GameObject touch;
}
