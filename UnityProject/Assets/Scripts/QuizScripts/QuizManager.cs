using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuizManager : MonoBehaviour {

	public static QuizManager instance;

	public Canvas questionScreen;

	public Text c1;
	public Text c2;
	public Text questionText;
	public Text questionAnsweredText;
	public Text resultText;
	public Text pointsText;
	public int answeredQuestionCount = 0;
	public int rightAnsweredCount = 0;
	public int points = 0;
	private Vector2 inputPos;
	// Use this for initialization

	private QuizItem question;
	public QuizUI quizUI;
	public Camera closeUpCamera;

	public List<QuizItem> questions;

	private bool firstFrame = true;

	public GameObject questionPanel;
	public GameObject answerPanel;
	public GameObject endGamePanel;
	public Text endGameResultText;

	public int rightAnswerPoint = 25;

	void InitQuiz()
	{
		answeredQuestionCount = 0;
		rightAnsweredCount = 0;
		points = 0;

		SetQuestionAnsweredText(answeredQuestionCount, questions.Count);
	}

	void Awake()
	{
		instance = this;

		InitQuiz();
	}

	void Start () {
		
	}

	public void AnswerSubmitted(int answer)
	{
		questionPanel.SetActive(false);
		answerPanel.SetActive(true);

		if(answer == question.question.rightAnswerIndex)
		{
			resultText.text = "CORRECT!";
			rightAnsweredCount++;
			points = rightAnsweredCount * rightAnswerPoint;
		}
		else
		{
			resultText.text = "WRONG :(";
		}

		question.question.answered = true;
		answeredQuestionCount++;
		endGameResultText.text = "Your Point: " + rightAnsweredCount*rightAnswerPoint + " / " + questions.Count*rightAnswerPoint;
		SetQuestionAnsweredText(answeredQuestionCount, questions.Count);

		if(answeredQuestionCount >= questions.Count)
			endGamePanel.SetActive(true);
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
		questionScreen.gameObject.SetActive(true);
		questionPanel.SetActive(true);
		answerPanel.SetActive(false);
		questionText.text = question.question.questionString;
		c1.text = question.question.choices[0];
		c2.text = question.question.choices[1];
		questionScreen.gameObject.GetComponent<lookat>().hoverTarget = question.gameObject.transform;

		closeUpCamera.transform.position = question.question.renderCamPos.position;
		closeUpCamera.transform.LookAt(question.gameObject.transform.position);
	}

	void SetQuestionAnsweredText(int answeredCount, int questionCount)
	{
		questionAnsweredText.text = "Questions Answered: " + answeredCount + "/" + questionCount;
		pointsText.text = "Points : " + points;
	}
	
	// Update is called once per frame
	void Update () {

		if(firstFrame)
		{
			SetQuestionAnsweredText(answeredQuestionCount, questions.Count);
			firstFrame = false;
		}

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
					if(question != null)
						if(!question.question.answered)
							AskQuestion();
				}
			}
		}
	}
}
