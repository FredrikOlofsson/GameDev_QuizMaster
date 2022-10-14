using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] Quiz quiz;
    [SerializeField] GameOver gameOver;
    void Start()
    {
        quiz.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
    }
    void Update()
    {
        if (quiz.gameOver == true) {
            quiz.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(true);
            gameOver.showGameOverText();
        }
    }
    public void OnReplayLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnExitGame() {
        Application.Quit();
    }
}
