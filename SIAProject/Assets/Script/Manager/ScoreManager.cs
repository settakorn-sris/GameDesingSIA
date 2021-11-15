using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField]private TextMeshProUGUI scoreText;

    public TextMeshProUGUI GetScoreText
    {
        get
        {
            return scoreText;
        }
    }

    public int Score { get; private set; }
    void Awake()
    {
        Score = 0;
        scoreText.text = $"{Score}";
    }
       
    public void AddScore(int score)
    {
        Score += score;
        scoreText.text = $"{Score}";
    }
    public void Rescore()
    {
        Score = 0;
        scoreText.text= $"{Score}";
    }
    public void MinusScore(int score)
    {
        Score -= score;
        scoreText.text = $"{Score}";
    }
}
