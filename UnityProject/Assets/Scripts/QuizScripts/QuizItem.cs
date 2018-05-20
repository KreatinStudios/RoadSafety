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

		public bool answered = false;
		public GameObject hint;
	}

	public QuizItemm question;

	// Use this for initialization
	void Start () {
		QuizManager.instance.questions.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
