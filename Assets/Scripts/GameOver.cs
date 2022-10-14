using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] ScoreKeeper scoreKeeper;

    public void showGameOverText()
    {
        finalScoreText.text = "Congratulations!\nYour final score was: " + scoreKeeper.CalculateScore() + "%";
    }
}
