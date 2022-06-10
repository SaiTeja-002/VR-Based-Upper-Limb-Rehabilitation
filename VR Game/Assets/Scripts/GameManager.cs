using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        // BackToMenuScript.BackToMenuAction += TeleportToMenu;
    }

    void OnDisable()
    {
        OptionHandler.TeleportAction -= Teleport;
        // BackToMenuScript.BackToMenuAction -= TeleportToMenu;
    }

    void Teleport()
    {
        if(SceneManager.GetActiveScene().name == "MenuScene")
            SceneManager.LoadScene("FishingScene");

        else if(SceneManager.GetActiveScene().name == "FishingScene")
            SceneManager.LoadScene("MenuScene");
    }

    void TeleportToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
