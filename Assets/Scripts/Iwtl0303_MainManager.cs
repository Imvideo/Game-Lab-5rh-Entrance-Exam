using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NUnit.Framework;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class Iwtl0303_MainManager : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text BestScoreText;
    public TMP_Text TimeText;

    public GameObject GameOverText;
    public GameObject WinText;
    public GameObject MenuObjects;
    public GameObject Player;
    public GameObject DeathEffectPrefab;
    public float deathEffectLife = 1.5f;

    private int currentScore;
    public bool isGameOver = false;
    public bool isWin = false;
    public string PlayerName;
    public int BestScore;
    public string BestPlayerName;

    public float winTime = 30f;
    private float elapsedTime = 0f; // 경과 시간
    public static Iwtl0303_MainManager Instance { get; private set; }

    public int ultimateNeedKills = 10;
    private int ultimateKills = 0;

    void Awake()
    {
        Instance = this;
        LoadBestScore();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BestScoreText.text = $"Best Score : {BestPlayerName} : {BestScore}";
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && !isWin)
        {
            elapsedTime += Time.deltaTime;
            float remain = Mathf.Max(0, winTime - elapsedTime);
            TimeText.text = $"Time : {Mathf.CeilToInt(remain)}";
            if (elapsedTime >= winTime)
            {
                Win();
            }
        }
    }

    public void AddPoint(int point)
    {
        currentScore += point;
        ScoreText.text = $"Score : {currentScore}";
    }

    public void SaveScore()
    {
        string currentName = (Iwtl0303_PersistentData.Instance != null && !string.IsNullOrWhiteSpace(Iwtl0303_PersistentData.Instance.PlayerName))
    ? Iwtl0303_PersistentData.Instance.PlayerName
    : "JYS";

        if (currentScore > BestScore)
        {
            BestScore = currentScore;
            BestPlayerName = currentName;
            SaveBestScore();
            BestScoreText.text = $"Best Score : {BestPlayerName} : {BestScore}";
        }
    }


    public void GameOver()
    {
        if (isWin) return;
        isGameOver = true;
        GameOverText.SetActive(true);
        MenuObjects.SetActive(true);


        SpawnDeathEffect(Player.transform.position);
        Destroy(Player);
        SaveScore();
    }

    public void Win()
    {
        if (isGameOver) return;
        isWin = true;
        WinText.SetActive(true);
        MenuObjects.SetActive(true);

        SaveScore();
    }

    void SpawnDeathEffect(Vector2 pos)
    {
        var fx = Instantiate(DeathEffectPrefab, pos, Quaternion.identity);
        Destroy(fx, deathEffectLife);
    }

    public void ActiveMenu()
    {

    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }



    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string bestPlayerName;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestScore = BestScore;
        data.bestPlayerName = BestPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScore = data.bestScore;
            BestPlayerName = data.bestPlayerName;
        }
    }
}
