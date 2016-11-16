using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		touch_front = transform.FindChild ("touch_front").gameObject;
		touch_front.SetActive (false);
		//initialize
		angle = 90;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0) && clicked)
		{
			Vector3 targetPos = Input.mousePosition;
			Vector3 mePos = Camera.main.WorldToScreenPoint (transform.position);
			angle = Mathf.Atan2 (targetPos.y-mePos.y, targetPos.x-mePos.x)/Mathf.PI*180;

			if (angle < 0)
				angle = 360 + angle;
			ChangeTouchFront ();
		}

		if (Input.GetMouseButtonUp (0) && clicked)
		{
			clicked = false;
			touch_front.SetActive (false);
		}
	}

	void ChangeTouchFront()
	{
		Vector3 mePos = Camera.main.WorldToScreenPoint (transform.position);
		touch_front.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
	}

	void OnMouseDown()
	{
		clicked = true;
		touch_front.SetActive (true);
	}
		
	static public TouchController instance;		//static instance
	public float angle;							//game angle;
	public bool clicked;						//clicked flag

	GameObject touch_front;						//touch front
}
