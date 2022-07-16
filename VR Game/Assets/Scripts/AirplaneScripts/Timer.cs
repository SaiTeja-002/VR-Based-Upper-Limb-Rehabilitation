using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timeElapsed;
    public Text timeElapsedText;

    [SerializeField]
    private TextMeshProUGUI minute0;
    [SerializeField]
    private TextMeshProUGUI minute1;
    // [SerializeField]
    // private TextMeshProUGUI minute2;
    [SerializeField]
    private TextMeshProUGUI second0;
    [SerializeField]
    private TextMeshProUGUI second1;

    void OnEnable()
    {
        GameOverCanvasScript.RetryAction += Reset;
    }

    void OnDisable()
    {
        GameOverCanvasScript.RetryAction += Reset;
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        ShowTimeElapsed();
    }

    void ShowTimeElapsed()
    {
        float minutesElapsed = Mathf.FloorToInt(timeElapsed/60);
        float secondsElapsed = Mathf.FloorToInt(timeElapsed%60);

        // timeElapsedText.text = string.Format("{0:00}:{1:00}", minutesElapsed, secondsElapsed);
        string timeElapsedString = string.Format("{00:00}{1:00}", minutesElapsed, secondsElapsed);

        minute0.text = timeElapsedString[0].ToString();
        minute1.text = timeElapsedString[1].ToString();
        second0.text = timeElapsedString[2].ToString();
        second1.text = timeElapsedString[3].ToString();
    }

    void Reset()
    {
        timeElapsed = 0f;
    }
}
