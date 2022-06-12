using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //Exit Code
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Saving Progress");
            Application.Quit();
        }
    }

    void OnEnable()
    {
        OptionHandler.TeleportAction += Teleport;
    }

    void OnDisable()
    {
        OptionHandler.TeleportAction -= Teleport;
    }

    void Teleport(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
