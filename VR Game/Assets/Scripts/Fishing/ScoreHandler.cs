using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    private string gameText = null;
    private TextMeshPro display;
    
    //Default Score
    void Start()
    {
        gameText = "Score: 0";
        display = GetComponent<TextMeshPro>();
        display.text = gameText;
    }

    //Updating Score When Caught
    void UpdateScore()
    {
        int score = int.Parse(gameText.Split(' ')[1]) + 1;
        gameText = "Score: " + score.ToString(); 
        display.text = gameText;
    }

    void OnEnable()
    {
        FishController.FishCaught += UpdateScore; 
    }

    void OnDisable()
    {
        FishController.FishCaught -= UpdateScore; 
    }
}
