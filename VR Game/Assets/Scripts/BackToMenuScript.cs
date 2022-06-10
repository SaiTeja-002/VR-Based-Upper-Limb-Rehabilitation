using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackToMenuScript : MonoBehaviour
{
    public float gazeCount = 5.0f;
    public bool isGazed;

    public static Action BackToMenuAction = null; 

    // Start is called before the first frame update
    void Start()
    {
        offState();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGazed)
            gazeCount -= Time.deltaTime;

        if(gazeCount <= 0)
        {
            Debug.Log("Going back to the Main Menu");

            if(BackToMenuAction != null)
                BackToMenuAction();

            gazeCount = 5.0f;
        }
    }

    public void offState()
    {
        isGazed = false;
    }

    public void onState()
    {
        isGazed = true;
    }
}
