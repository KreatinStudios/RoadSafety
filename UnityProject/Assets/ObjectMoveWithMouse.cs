using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveWithMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(2))
			Debug.Log("scroll button hit");
	}
}
