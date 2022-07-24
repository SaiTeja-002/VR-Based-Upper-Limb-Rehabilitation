using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    
    [SerializeField] private int increment;

    [SerializeField] private TextMeshProUGUI scoreTMP;

    public ObstacleManager obstacleManager;

    void OnEnable()
    {
        ObstacleManager.ObstacleDodgedAction += IncrementScore;
        PlayerController.PlayerDeadAction += ResetScore;
    }

    void OnDisable()
    {
        ObstacleManager.ObstacleDodgedAction -= IncrementScore;
        PlayerController.PlayerDeadAction -= ResetScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IncrementScore()
    {
        score += increment;
        DisplayScore();
    }

    void DisplayScore()
    {
        scoreTMP.text = "Score : " + score.ToString();
    }

    void ResetScore()
    {
        score = 0;
        Invoke("DisplayScore", 2f);
    }
}
