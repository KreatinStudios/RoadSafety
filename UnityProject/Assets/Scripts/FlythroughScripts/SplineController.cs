using UnityEngine;
using System.Collections;

public class SplineController : MonoBehaviour
{
    Spline spline;
	
	public GameObject path;
	public bool lookAtNextPoint = true;
	
	float dt = 0;
    public float pathTime = 120.0f;
    public float StartTime = 0.0f;

	public float heightOffset = 0.0f;
    
    int nextPoint = 0;
    Quaternion nextRot;
    float totalDistance = 0;
    float currentDistance = 0;
	float dy = 0;
	float dx = 0;
	float incrementY = 0;
	float incrementX = 0;
	float initialY = 0;
	float initialX = 0;
	float tx = 0;
	float ty = 0;
	Vector3 rotAngle;
	Vector3 prevPos;
	
	float newY;
	float newX;
	
	float my;
	float mx;
	
	float spd = 0;
	
	public Animator anm;
	
	public bool effectAnimation = true;
    public bool stickToGround = false;

    public float playBackCoeff = 1.0f;
	
	private Transform myTransform;

    public int playCount = 0;

    private int tourCount = 0;
	//public bool ignoreY = false;
	
	private float initialYPos;
    private bool endTour = false;

    public float speed = 1.0f;

	public bool manualControl = false;
	public float manualControlTime = 0.0f;
	void Awake()
	{

        spline = new Spline(path, stickToGround);
        prevPos = transform.position;
		//transform.rotation = spline.GetRotation(0);
		nextRot = spline.GetRotation(0);
		initialYPos = transform.position.y;
		totalDistance = Vector3.Distance(transform.position, spline.GetPoint(nextPoint));
		
    }

    void StickToGround()
    {
        RaycastHit hitInfo;
        Vector3 p = transform.position;
        Physics.Raycast(new Vector3(p.x,p.y+10,p.z), Vector3.down, out hitInfo);
        //Physics.SphereCast(gameObject.transform.position,100, Vector3.down, out hitInfo);
        //Debug.Log(hitInfo.point.y);
        Quaternion rot = transform.rotation;
        transform.position = new Vector3(p.x, hitInfo.point.y, p.z);
        transform.rotation = rot;
    }
    
    void Start()
	{
			myTransform = transform;
	}
    
    Quaternion Hermite(Quaternion start, Quaternion end, float value)
    {
        return Quaternion.Lerp(start, end, value * value * (3.0f - 2.0f * value));
    }
    
    void lookAtFunc(float dt)
    {
    	float dist = Vector3.Distance(myTransform.position, spline.GetPoint(nextPoint));
    	
    	if(dist<5.0f)
    	{
    		nextPoint++;
    		if(nextPoint >= spline.numPoints - 2)
    			nextPoint = 0;
	
    		nextRot = spline.GetRotation(nextPoint);
    		totalDistance = Vector3.Distance(myTransform.position, spline.GetPoint(nextPoint));
			rotAngle = nextRot.eulerAngles;
			
			dy = rotAngle.y - myTransform.rotation.eulerAngles.y;
			dx = rotAngle.x - myTransform.rotation.eulerAngles.x;
			
			if(Mathf.Abs(dy) > 180)
			{
				if(dy < 0)
					dy = dy + 360;
				else 
					dy = dy - 360;
			}

			if(Mathf.Abs(dx) > 180)
			{
				if(dx < 0)
					dx = dx + 360;
				else 
					dx = dx - 360;
			}
			
			initialY = myTransform.rotation.eulerAngles.y;
			initialX = myTransform.rotation.eulerAngles.x;
			
			my = dy/totalDistance;
			mx = dx/totalDistance;
			
    	}	
		currentDistance = totalDistance - Vector3.Distance(myTransform.position, spline.GetPoint(nextPoint));
		
		incrementY = 0;
		incrementX = 0;
		
		if(totalDistance > 0)
		{			
			incrementY = currentDistance*my;
			incrementX = currentDistance*mx;
		}

		
		newY = initialY+incrementY;
		newX = initialX+incrementX;
		
		myTransform.rotation = Quaternion.Euler(newX,newY,0);
		
    }

	void LateUpdate()
	{
		if (manualControl) {
			dt = manualControlTime;
			CalculatePos();
		}

		float y = transform.position.y;
		transform.position = new Vector3(transform.position.x, y + heightOffset, transform.position.z);
	}

	void CalculatePos()
	{
		Vector3 next_position = spline.GetPosition((dt + StartTime)/pathTime);
		
		
		
		Vector3 temppos = Vector3.Lerp(myTransform.position, next_position, 1);
		
		Vector3 current_look_vec = myTransform.TransformDirection(Vector3.forward);
		Vector3 look_dir = next_position - myTransform.position;
		
		myTransform.position = temppos;
		
		
		if(lookAtNextPoint)
		{
			myTransform.LookAt(temppos + Vector3.Slerp(current_look_vec, look_dir, 1));
		}
		else
			lookAtFunc(Time.deltaTime);

	}

	void Update ()
	{
	    if (endTour) return;

	    if(spline.Cut(spline.GetCurrentId(dt/pathTime)))
		{
			dt += spline.delta_t * pathTime * speed;
            
		}
				
	        
		/*Vector3 next_position = spline.GetPosition((dt + StartTime)/pathTime);
		
		
			
	    Vector3 temppos = Vector3.Lerp(myTransform.position, next_position, 1);
        
        Vector3 current_look_vec = myTransform.TransformDirection(Vector3.forward);
        Vector3 look_dir = next_position - myTransform.position;

        myTransform.position = temppos;
                
		
	    if(lookAtNextPoint)
		{
            myTransform.LookAt(temppos + Vector3.Slerp(current_look_vec, look_dir, 1));
		}
	    else
	        lookAtFunc(Time.deltaTime);

        /*if (stickToGround)
        {
            StickToGround();
        }*/
		CalculatePos ();


        dt += Time.deltaTime * speed;

	    if ((dt + StartTime) > pathTime)
	    {
            dt = -StartTime;
	        tourCount++;
            if((tourCount >= playCount) && (playCount != 0))
                GoAway();
	        //Debug.Log("bitirdim bir turu" + gameObject.name);
	    }
            

		spd = Mathf.Lerp(spd, Vector3.Distance(myTransform.position , prevPos) / Time.deltaTime, 0.05f);
//	        Debug.Log(spd);
	    if(effectAnimation)
            if(anm != null)
                anm.SetFloat("speed", speed * playBackCoeff);
        
	    
		
		prevPos = myTransform.position;
	}

    void GoAway()
    {
        Vector3 temp = transform.position;
        temp.y -= 1000;
        gameObject.transform.position = temp;
        endTour = true;
    }
	
	void OnDrawGizmosSelected()
    {

        spline = new Spline(path, stickToGround);
		//Debug.Log(spline.delta_t * pathTime);
		//Debug.Log("gizmo drawing");
        for (float t = 0; t < 1f; t += 0.01f)
		{
			if(spline.Cut(spline.GetCurrentId(t)))
			{
				//Debug.Log("var burada kesik");
				t += spline.delta_t;
			}
		    Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(spline.GetPosition(t), 0.01f);
		}
    }
	
}
