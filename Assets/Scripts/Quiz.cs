using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;
using Slider = UnityEngine.UI.Slider;

public class Quiz : MonoBehaviour {
    [Header("Questions")]
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] int correctAnswerIndex;
    bool hasAnswerdEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite falseAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    [SerializeField] Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] ScoreKeeper scoreKeeper;

    [SerializeField] Slider progressBar;

    private int currentScore;
    public bool gameOver;

    void Start() {
        progressBar.value = 0; 
        progressBar.maxValue = questions.Count-1;
        
    }
    void Update() {
        if (gameOver == true) {
            Debug.Log("Game Over!");
            gameOver = false;
        }
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion == true) {
            if (progressBar.value == progressBar.maxValue) {
                gameOver = true;
            }
            hasAnswerdEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if (hasAnswerdEarly == false && timer.isAnsweringQuestion == false) {
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
            TextMeshProUGUI buttonText = answerButtons[correctAnswerIndex].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "False!";
            //Pressed button > change the border to red
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = falseAnswerSprite;
            //HighLights the right answer
            Image correctButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
        }
        scoreKeeper.IncrementQuestionsSeen();
    }
    public void OnAnswerSelected(int index) {
        hasAnswerdEarly = true;
        DisplayRightAnswer(index);
        timer.SetTimerToZero();
        setButtonState(false);
        progressBar.value++;
    }
}