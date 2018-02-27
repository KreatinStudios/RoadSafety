using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuizManager : MonoBehaviour {



	public Canvas questionScreen;

	public Text c1;
	public Text c2;
	public Text questionText;
	private Vector2 inputPos;
	// Use this for initialization

	private QuizItem question;
	public Camera closeUpCamera;
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


	void AskQuestion()
	{
		questionText.text = question.question.questionString;
		c1.text = question.question.choices[0];
		c2.text = question.question.choices[1];
		questionScreen.gameObject.GetComponent<lookat>().hoverTarget = question.gameObject.transform;

		closeUpCamera.transform.position = question.question.renderCamPos.position;
		closeUpCamera.transform.LookAt(question.gameObject.transform.position);
		questionScreen.gameObject.SetActive(true);


	}
	
	// Update is called once per frame
	void Update () {

		if(question!= null)
		{
			closeUpCamera.transform.position = question.question.renderCamPos.position;
			closeUpCamera.transform.LookAt(question.gameObject.transform.position);
		}
		if (CheckInput()) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(inputPos);
			if (Physics.Raycast(ray, out hit))
			{
				if(hit.transform.gameObject.layer == 11)
				{
					question = hit.transform.gameObject.GetComponent<QuizItem>();
					AskQuestion();
				}
			}



		}
	}
}
