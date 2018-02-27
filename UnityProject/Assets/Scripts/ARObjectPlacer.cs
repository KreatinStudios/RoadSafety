using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObjectPlacer : MonoBehaviour {


	public GameObject obj;
	// Use this for initialization
	private bool objPlaced = false;
	private Vector2 inputPos;
	void Start () {
		
	}



	bool CheckInput()
	{
		if(Input.GetMouseButtonUp(0))
		{
			inputPos = Input.mousePosition;
			return true;
		}

		if(Input.touchCount > 0)
		{
			inputPos = Input.GetTouch(0).position;
			return true;
		}

		return false;

	}
	
	// Update is called once per frame
	void Update () {
		if(objPlaced) return;
		//return;
		if (CheckInput()) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(inputPos);
			if (Physics.Raycast(ray, out hit))
			{
				if(hit.transform.gameObject.layer == 10)
					obj.transform.position = hit.point;

				obj.SetActive(true);
				objPlaced = true;
			}



		}
	}
}
