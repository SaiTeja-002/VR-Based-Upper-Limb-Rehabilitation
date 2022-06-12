using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OptionHandler : MonoBehaviour
{
    // Actions
    public static Action<string> TeleportAction = null;
    public static Action<string> IsGazingAction = null;
    public static Action<string> IsNotGazingAction = null;

    public Material onColor;
    public Material ofColor;
    public float gazeCount = 5.0f;
    private bool optionGaze;

    //Default State of Option : OF
    void Start()
    {   
        OfState();
    }

    //Teleports When Conditions Are Met
    void Update()
    {
        //Option Choosing Logic
        if(optionGaze)
        {
            gazeCount -= Time.deltaTime;

            // Subscribing to the IsGazingAction
            if(IsGazingAction != null)
                IsGazingAction(gameObject.tag);
        }

        else
        {
            // Subscribing to the IsNotGazingAction
            if(IsNotGazingAction != null)
                IsNotGazingAction(gameObject.tag);
        }

        if(gazeCount <= 0)
        {
            if(TeleportAction != null)
                TeleportAction(gameObject.tag);
                
            gazeCount = 5.0f;
        }
    }

    //Parameters When Option is Not Gazed
    public void OfState()
    {
        gazeCount = 5.0f;
        optionGaze = false;
        GetComponent<Renderer>().material = ofColor;
    }

    //Parameters When Option is Not Gazed
    public void OnState()
    {
        gazeCount = 5.0f;
        optionGaze = true;
        GetComponent<Renderer>().material = onColor;
    }
}
