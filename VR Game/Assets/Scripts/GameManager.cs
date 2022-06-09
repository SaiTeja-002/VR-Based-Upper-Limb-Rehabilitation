using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
}
