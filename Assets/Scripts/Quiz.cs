using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour {
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    void Start() {
        GetNextQuestion();
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
        Debug.LogWarning("In Display!");
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI childButtonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            childButtonText.text = question.GetAnswers(i);
        }
    }
    public void OnAnswerSelected(int index) {
        if (index == question.GetCorrectAnswerIndex()) {
            questionText.text = "Correct!";

            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        } else {
            TextMeshProUGUI buttonText = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "False! it was: " + question.GetAnswers(correctAnswerIndex);
            Image correctButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
            Debug.LogWarning("Not the right answer!");
        }
        setButtonState(false);
    }
}
