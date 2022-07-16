using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOverCanvasScript : MonoBehaviour
{
    public static Action ExitAction = null;
    public static Action RetryAction = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }
    }

    public void Retry()
    {
        Debug.Log("Retry is pressed");

        if(RetryAction != null)
        {
            RetryAction();
        }
    }

    public void Settings()
    {
        Debug.Log("Settings is pressed");
    }

    public void Exit()
    {
        Debug.Log("Exit is pressed");

        if(ExitAction != null)
        {
            ExitAction();
        }
    }
}
