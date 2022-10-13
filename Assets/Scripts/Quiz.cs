using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour {
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] int correctAnswerIndex;
    bool hasAnswerdEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start() {
        correctAnswerIndex = question.GetCorrectAnswerIndex();
        GetNextQuestion();
        timer = FindObjectOfType<Timer>();
    }
    void Update() {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion) {
            hasAnswerdEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if (hasAnswerdEarly == false && timer.isAnsweringQuestion == false) {
            DisplayAnswer(-1);
            setButtonState(false);
        }
    }

    public void setButtonState(bool state) {
        for (int i = 0; i < answerButtons.Length; i++) {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void GetNextQuestion() {
        setButtonState(true);
        SetButtonSpriteDefault();
        DisplayQuestion();
    }

    private void SetButtonSpriteDefault() {
        for (int i = 0; i < answerButtons.Length; i++) {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    public void DisplayQuestion() {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < 4; i++) {
            TextMeshProUGUI childButtonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            childButtonText.text = question.GetAnswers(i);
        }
    }
    void DisplayAnswer(int index) {
        if (index == question.GetCorrectAnswerIndex()) {
            questionText.text = "Correct!";

            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        } else {
            TextMeshProUGUI buttonText = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "False! it was: " + question.GetAnswers(correctAnswerIndex);
            Image correctButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
        }
    }
    public void OnAnswerSelected(int index) {
        hasAnswerdEarly = true;
        DisplayAnswer(index);
        timer.CancelTimer();
        setButtonState(false);
    }
}
