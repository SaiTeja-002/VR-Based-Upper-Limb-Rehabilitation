using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionHandler : MonoBehaviour
{
    public Material onColor;
    public Material ofColor;
    private float gazeCount;
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
            gazeCount -= Time.deltaTime;

        if(gazeCount <= 0)
        {
            Debug.Log("Teleport\n");
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
