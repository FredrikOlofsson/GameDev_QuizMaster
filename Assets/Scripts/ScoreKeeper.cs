using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {
    public int correctAnswers;
    public int questionsSeen;

    void Start() {
        correctAnswers = 0;
        questionsSeen = 0;
    }
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
    }
    public int CalculateScore() {
        if(questionsSeen == 0) return 0;
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
