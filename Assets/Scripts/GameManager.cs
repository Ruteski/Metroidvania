using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//SINGLETON
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    public int score;
    public Text scoreText;

    public static GameManager instance;

    private void Awake() {
        instance = this;
        //DontDestroyOnLoad(this);

        //if (instance == null) {
        //    instance = this;
        //} else {
        //    Destroy(gameObject);
        //}

        Time.timeScale = 1;

        if (PlayerPrefs.GetInt("Score") > 0) {
            scoreText.text = "x " + PlayerPrefs.GetInt("Score").ToString();
        }
    }

    public void GetCoin() {
        score++;
        scoreText.text =  "x " + score.ToString();

        PlayerPrefs.SetInt("Score", score);
    }

    public void NextLvl() {
        SceneManager.LoadScene(1);
    }

    public void ShowGameOver() {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);        
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
