using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineRandomizer : MonoBehaviour {



	public SplineController[] splines;
	// Use this for initialization
	void Start () {
		splines = GetComponentsInChildren<SplineController>();

		int i = 0;
		float dice;
		for(i=0;i<splines.Length;i++)
		{
			dice = Random.Range(0,0.5f);

			if(dice < 0.5f)
				splines[i].speed = -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
