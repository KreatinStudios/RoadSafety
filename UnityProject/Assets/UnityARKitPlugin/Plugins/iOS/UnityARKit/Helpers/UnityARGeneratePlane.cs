using System;
using System.Collections.Generic;
using UnityEngine.Events;
namespace UnityEngine.XR.iOS
{
	public class UnityARGeneratePlane : MonoBehaviour
	{
		public GameObject planePrefab;
        private UnityARAnchorManager unityARAnchorManager;

        public int planeCount = 0;

        public UnityEvent firstPlaneCreated;

		// Use this for initialization
		void Start () {
            unityARAnchorManager = new UnityARAnchorManager();
			UnityARUtility.InitializePlanePrefab (planePrefab);

            planeCount = 0;
		}

        void OnDestroy()
        {
            unityARAnchorManager.Destroy ();
        }


        void CheckFirstPlane()
        {
            List<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors ();
            
            if(planeCount == 0 && arpags.Count > 0)
            {
                planeCount = arpags.Count;
                if(firstPlaneCreated!=null)
                    firstPlaneCreated.Invoke();
            }
        }

        void Update()
        {
            CheckFirstPlane();
        }

        /*void OnGUI()
        {
            List<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors ();
            if (arpags.Count >= 1) {
                //ARPlaneAnchor ap = arpags [0].planeAnchor;
                //GUI.Box (new Rect (100, 100, 800, 60), string.Format ("Center: x:{0}, y:{1}, z:{2}", ap.center.x, ap.center.y, ap.center.z));
                //GUI.Box(new Rect(100, 200, 800, 60), string.Format ("Extent: x:{0}, y:{1}, z:{2}", ap.extent.x, ap.extent.y, ap.extent.z));
            }
        }*/
	}
}

