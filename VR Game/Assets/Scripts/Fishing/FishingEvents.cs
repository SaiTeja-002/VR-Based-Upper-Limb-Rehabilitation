using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishingEvents : MonoBehaviour
{ 
    public static Action RodThrown = null;
    public static Action RodPulled = null;

    private bool toggleAction;
        
    //First Change in Rotation : Throw
    void Start()
    {
        toggleAction = true;
    }

    //Constatnly Checks For Rotation
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space))
            ForwardAction();
    }

    //Triggers The Required Action
    void ForwardAction()
    {
        if(toggleAction)
            RodThrown();
        else
            RodPulled();
        
        toggleAction = !toggleAction;
    }
}
