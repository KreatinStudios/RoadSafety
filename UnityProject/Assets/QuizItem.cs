using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizItem : MonoBehaviour {

	[System.Serializable]
	public class QuizItemm
	{
		public string questionString;
		public string[] choices;

		public int rightAnswerIndex;
		public Transform renderCamPos;
	}

	public QuizItemm question;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
