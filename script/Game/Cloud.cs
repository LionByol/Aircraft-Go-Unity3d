using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		//initialize
		int rnd = Random.Range(0, clouds.Length);
		GetComponent<SpriteRenderer> ().sprite = clouds [rnd];
		GetComponent<SpriteRenderer> ().color = new Color32 ((byte)255, (byte)255, (byte)255, (byte)Random.Range (100, 200));
		float srnd = Random.Range(0.5f, 1f);
		transform.localScale = new Vector3 (srnd, srnd, 1);
		width = (float)GetComponent<SpriteRenderer>().bounds.size.x;

		//decide position
		float angle = Aircraft.instance.angle;
		float x=0, y=0;
		if (angle > 0 && angle < 90) {
			rnd = Random.Range(0, 90);
		}else if (angle > 90 && angle < 180) {
			rnd = Random.Range(90, 180);
		}else if (angle > 180 && angle < 270) {
			rnd = Random.Range(180, 270);
		}else if (angle > 270 && angle < 360) {
			rnd = Random.Range(270, 360);
		}else if (angle == 0 || angle==360) {
			rnd = Random.Range(315, 405);
		}else if (angle == 90) {
			rnd = Random.Range(45, 135);
		}else if (angle == 180) {
			rnd = Random.Range(135, 225);
		}else if (angle == 270) {
			rnd = Random.Range(225, 315);
		}
		x = World.instance.screenHeight / 2 * Mathf.Cos(rnd*Mathf.PI/180);
		y= World.instance.screenHeight / 2 * Mathf.Sin(rnd*Mathf.PI/180);

		transform.position = new Vector3 (x, y, transform.position.z);

		StartCoroutine (CheckDestroyCloud ());
	}

	// Update is called once per frame
	void Update ()
	{
		float angle = Aircraft.instance.angle;
		float speed = Aircraft.instance.speed*transform.localScale.x*width;
		Vector3 direction = -new Vector3 (Mathf.Cos(angle*Mathf.PI/180), Mathf.Sin(angle*Mathf.PI/180), 0);
		transform.Translate (direction*speed);
	}

	IEnumerator CheckDestroyCloud()
	{
		yield return new WaitForSeconds (10f);
		StartCoroutine (DestroyCloud());
	}

	IEnumerator DestroyCloud()
	{
		while (true)
		{
			yield return new WaitForSeconds (0.05f);
			float al = gameObject.GetComponent<SpriteRenderer> ().color.a;
			al -= 0.025f;
			gameObject.GetComponent<SpriteRenderer> ().color = new Color32((byte)255, (byte)255, (byte)255, (byte)(al*255));
			if (al < 0)
				break;
		}
		Destroy (gameObject);
	}

	float width;

	public Sprite[] clouds;
}
