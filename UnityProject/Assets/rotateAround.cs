using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour {


	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.RotateAround(transform.position, new Vector3(0,0,1), speed * Time.deltaTime);

	}
}
