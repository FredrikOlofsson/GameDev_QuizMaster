using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour {




    [Header("Questions")]
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;
    //QUESTION Is it good practice to first make it serializefield, add object then remove the serializefield?

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] int correctAnswerIndex;
    bool hasAnswerdEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite falseAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    private int currentScore;

    void Start() {
        timer = FindObjectOfType<Timer>(); //QUESTION How does it know that it's the right timer?
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Update() {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion == true) {
            hasAnswerdEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if (hasAnswerdEarly == false && timer.isAnsweringQuestion == false) {
            //DisplayRightAnswer(-1); //QUESTION Why was this implemented?
            setButtonState(false);
        }

        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

    }

    void GetNextQuestion() {
        if (questions.Count > 0) {
            setButtonState(true);
            SetButtonSpriteDefault();
            GetRandomQuestion();
            DisplayQuestionAndAnswers();
        } else {
            questionText.text = "Game Over! \nScore : " + currentScore;
            timer.enabled = false;
        }

    }
    public void setButtonState(bool state) {
        for (int i = 0; i < answerButtons.Length; i++) {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetButtonSpriteDefault() {
        for (int i = 0; i < answerButtons.Length; i++) {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
    private void GetRandomQuestion() {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion)) {
            questions.Remove(currentQuestion);
        }

    }

    public void DisplayQuestionAndAnswers() {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI childButtonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            childButtonText.text = currentQuestion.GetAnswers(i);
        }
    }
    void DisplayRightAnswer(int index) {
        //Debug.Log("The right answer is button: " + (correctAnswerIndex + 1));
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswerIndex()) {
            //IMPLEMENT Each question should have a description/explanation of the answer.
            questionText.text = "Correct!";
            currentScore++;
            scoreKeeper.IncrementCorrectAnswers();


            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        } else {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            //Pressed button > change the text
            TextMeshProUGUI buttonText = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "False!";
            //Pressed button > change the border to red
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = falseAnswerSprite;
            //HighLights the right answer
            Image correctButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
        scoreKeeper.IncrementQuestionsSeen();
        }
    }
    public void OnAnswerSelected(int index) {
        //Debug.Log("User pressed the " + (index + 1) + " button");
        hasAnswerdEarly = true;
        DisplayRightAnswer(index);
        timer.SetTimerToZero();
        setButtonState(false);
    }
}
