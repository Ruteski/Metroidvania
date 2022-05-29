using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//SINGLETON
public class GameManager : MonoBehaviour
{
    public int score;
    public Text scoreText;

    public static GameManager instance;

    private void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetCoin() {
        score++;
        scoreText.text =  "x " + score.ToString();
    }
}
