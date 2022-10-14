using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {
    public int correctAnswers = 0;
    public int questionsSeen = 0;

    public int GetCorrectAnswers() {
        return correctAnswers;
    }
    public void IncrementCorrectAnswers() {
        correctAnswers++;
    }
    public int GetQuestionsSeen() {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen() {
        questionsSeen++;
        Debug.Log("I am incrementing the questions seen" + questionsSeen);
    }
    public int CalculateScore() {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}