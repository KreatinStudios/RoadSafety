using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armSwinger : MonoBehaviour {

	public GameObject leftShoulder;
	public GameObject rightShoulder;

	public float maxAngle = 30;
	public float speed = 1;
	private float t = 0;
	// Use this for initialization
	void Start () {
		t = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float rot = 0;
		t += Time.deltaTime * speed;
		rot = Mathf.Sin(t) * maxAngle;

		leftShoulder.transform.localEulerAngles = new Vector3( 0,0,rot);
		rightShoulder.transform.localEulerAngles = new Vector3( 0,0,-rot);
	}
}
