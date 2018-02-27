using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour {

	public Transform myTarget;
	public Transform hoverTarget;
	// Use this for initialization
	private float y;
	void Start () {
		y = gameObject.transform.position.y;
	}

	// Update is called once per frame
	void Update () {

		LookTo (myTarget);
		gameObject.transform.position = new Vector3(hoverTarget.position.x,y,hoverTarget.position.z);
	}


	private void LookTo(Transform target)
	{
		// Comment line below
		//Transform transform=null;
		if (target == null)
			return;

		var posDiff = target.position - transform.position;
		posDiff.y = 0;
		transform.LookAt(transform.position + posDiff, Vector3.up);
		//transform.Rotate (90, 0, 0);
	}
}
