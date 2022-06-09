using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DirectionalLightMotion : MonoBehaviour
{

    public float rotationalSpeed = 7.0f;    //The speed at which the DirectionalLight rotates
    
    public static Action LightOffAction = null; //Action used to specify that the DirectionalLight is not focused
    public static Action LightOnAction = null;  //Action used to specify that the DirectionalLight is focused

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime*rotationalSpeed, 0); //Rotating the DirectionalLight continuously
        
        float directionalLightY = transform.rotation.eulerAngles.y; //Angle of the DirectionalLight made with the Y-axis

        /* Angles at which the DirectionalLight is not foused */
        if(directionalLightY > 100.0f && directionalLightY < 260.0f)
        {
            if(LightOffAction != null)
                LightOffAction();
        }

        else    //Corresponds to the angles at which the DirectionalLight is focused
        {
            if(LightOnAction != null)
                LightOnAction();
        }
    }
}
