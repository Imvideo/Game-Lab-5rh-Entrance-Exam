using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Iwtl0303_MainManager : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text BestScoreText;
    public GameObject GameOverText;

    private int currentScore;
    public bool isGameOver = false;
    public string PlayerName;
    public int BestScore;
    public string BestPlayerName;

    public static Iwtl0303_MainManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BestScoreText.text = $"Best Score : {BestPlayerName} - {BestScore}";
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint(int point)
    {   
        currentScore += point;
        ScoreText.text = $"Score : {currentScore}";
    }

    public void GameOver()
    {
        isGameOver = true;
        GameOverText.SetActive(true);
    }
}
