using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spline : MonoBehaviour
{
	Component[] waypoints;
	ArrayList positions = new ArrayList();
	ArrayList rotations = new ArrayList();
	ArrayList skipList = new ArrayList();
	int cutCount = 0;
	public float delta_t = 0.0f;
    public bool groundSnap = false;
	public int numPoints
	{
		get { return positions.Count - cutCount; }
	}
	
	public Spline(GameObject path_object, bool stickToGround)
	{
		//Debug.Log("*****************************");
		List<Component> components = new List<Component>(path_object.GetComponentsInChildren<Waypoint>());

		components.Sort(delegate(Component a, Component b) { return a.name.CompareTo(b.name); });

		waypoints = components.ToArray();
		Waypoint wp;
		int i = 0;
		cutCount = 0;
		foreach (Component waypoint in waypoints)
		{
			positions.Add(waypoint.transform.position);
			rotations.Add(waypoint.transform.rotation);
			
			wp = waypoint.gameObject.GetComponent<Waypoint>();
			if(wp.CutHere)
			{
				skipList.Add(i);
				cutCount++;
			}
			i++;
			
		}
		
		if(numPoints > 2)
		{
			
			positions.Add(waypoints[0].transform.position);
			positions.Add(waypoints[1].transform.position);
			i++;
			rotations.Add(waypoints[0].transform.rotation);
			rotations.Add(waypoints[1].transform.rotation);
			i++;
		}
		
		delta_t = 1.0f / (i-2); //adjust spline end to penultimate control point . . .

	    groundSnap = stickToGround;
	}		
	
	public Quaternion GetRotation(int n)
	{
		return (Quaternion) rotations[n];
	}

    Vector3 GetGroundedPos(Vector3 p)
    {
        RaycastHit hitInfo;
        Physics.Raycast(new Vector3(p.x, p.y + 0.1f, p.z), Vector3.down, out hitInfo);
        p = new Vector3(p.x, hitInfo.point.y, p.z);
        return p;
    }

	public Vector3 GetPoint(int n)
	{
		return (Vector3) positions[n];
	}
	
	public bool Cut(int n)
	{
		int i = 0;
		bool result = false;
		for(i=0;i<cutCount;i++)
			if((int)skipList[i] == n)
				result = true;
		
		return result;
	}
	
	public int GetCurrentId(float spline_t)
	{
			int p = (int) (Mathf.Abs(spline_t) / delta_t);
			return p;
	}

	public Vector3 GetPosition(float spline_t)
	{
		 

	    int p = (int) (Mathf.Abs(spline_t) / delta_t);
		
		int p0 = Mathf.Clamp(p - 1, 0, this.numPoints-1);
	    int p1 = Mathf.Clamp(p, 0, this.numPoints-1);
	    int p2 = Mathf.Clamp(p + 1, 0, this.numPoints-1);
	    int p3 = Mathf.Clamp(p + 2, 0, this.numPoints-1);
		
		if  (p1==0)
		{
			p0=this.numPoints-1;
			p2=1;
			p3=2;
		}
		if  (p1==this.numPoints-1)
		{
			p2=0;
			p3=1;
		}

		if  (p1==this.numPoints-2)
		{
			p3=0;
		}

		float t = (spline_t - delta_t*p) / delta_t;
		
	    float t2 = t * t;
	    float t3 = t2 * t;
	
	    float b0 = .5f * (  -t3 + 2*t2 - t);
	    float b1 = .5f * ( 3*t3 - 5*t2 + 2);
	    float b2 = .5f * (-3*t3 + 4*t2 + t);
	    float b3 = .5f * (   t3 -   t2    );
		
	    Vector3 result = (this.GetPoint(p0)*b0 + this.GetPoint(p1)*b1 + this.GetPoint(p2)*b2 + this.GetPoint(p3)*b3);

        if (groundSnap)
	    {

            return GetGroundedPos(result);
	    }
            

	    return result;
	}

}
