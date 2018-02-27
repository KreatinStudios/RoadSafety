using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{
    public Quaternion look;
    
    GameObject cam;
    
    Quaternion originalRotation;
	
	public bool drawRay = true;
	public bool CutHere = false;
    
	Spline spline;
    void OnDrawGizmos()
    {        
        //Gizmos.DrawSphere(transform.position, 0.4f);
		if(drawRay)
		{
			Gizmos.color = Color.white;
			//Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * 35);
		}
    }

	void DrawWay()
	{
		spline = new Spline(gameObject.transform.parent.gameObject, false);
		//Debug.Log(spline.delta_t * pathTime);
		//Debug.Log("gizmo drawing");
		for (float t = 0; t < 1f; t += 0.01f)
		{
			if(spline.Cut(spline.GetCurrentId(t)))
			{
				//Debug.Log("var burada kesik");
				t += spline.delta_t;
			}
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(spline.GetPosition(t), 0.02f);
		}
	}
    
    
    void OnDrawGizmosSelected()
	{      
		if( drawRay )
		{
			Gizmos.color = Color.white;
			//Gizmos.
			//Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * 35);
		}

		DrawWay ();
	}
}

