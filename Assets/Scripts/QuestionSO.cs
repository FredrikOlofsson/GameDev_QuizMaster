using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject {
    [TextArea(2, 4)]
    [SerializeField] string question = "Insert Question";
    [TextArea(3, 5)]
    [SerializeField] string explaination = "Insert explanation of the answer";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;

    public string GetQuestion() {
        return question;
    }
    public int GetCorrectAnswerIndex() {
        return correctAnswerIndex;
    }
    public string GetAnswers(int index) {
        return answers[index];
    }
    public string GetExplaination() {
        return explaination;
    }
}