using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyBounce : MonoBehaviour {
	public float maxBounce = 0.01f;
	public float bounceSpeed = 11f;
		private float t = 0;
	private bool firstTime = true;

	private Vector3 defPos;
	// Use this for initialization
	void Start () {
		t = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(firstTime) return;
		t += Time.deltaTime* bounceSpeed;
		float y = Mathf.Sin(t) * maxBounce ;

		transform.localPosition = new Vector3(defPos.x, defPos.y + y, defPos.z);
	}

	void LateUpdate()
	{
		if(firstTime)
		{
			firstTime = false;
			defPos = transform.localPosition;
		}
	}
}
