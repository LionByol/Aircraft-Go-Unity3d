using UnityEngine;
using System.Collections;

public class Aircraft : MonoBehaviour {

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		//fade in
		gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 0);
		StartCoroutine (StartAircraft ());

		//initialize
		speed = 0.025f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//set plane angle
		SetAngle();
		//get angle of plane
		GetAngle();
	}

	public void SetAngle()
	{
		if (!TouchController.instance.clicked)
			return;
		float targetAngle = TouchController.instance.angle - 90;
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), 1f*Time.deltaTime);
	}

	public float GetAngle()
	{
		angle = transform.rotation.eulerAngles.z+90;
		if (angle > 360)
			angle %= 360f;
		return angle;
	}

	IEnumerator StartAircraft()
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

	static public Aircraft instance;
	public float angle;
	public float speed;
	public float turnSpeed;
}
