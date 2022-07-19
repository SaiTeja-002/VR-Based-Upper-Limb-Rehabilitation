using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishingEvents : MonoBehaviour
{ 
    public static Action RodThrown = null;
    public static Action RodPulled = null;

    private bool toggleAction;
        
    //Establishing Connection With HC-05
    void Start()
    {
        toggleAction = true;
        BluetoothService.CreateBluetoothObject();
        BluetoothService.StartBluetoothConnection("HC-05");
    }

    //Constantly Checks For Rotation
    void Update()
    {    
        //For Testing On PC
        if (Input.GetKeyDown(KeyCode.Space))
            RodThrown();
            
        else if (Input.GetKeyDown(KeyCode.R))
            RodPulled();

        //Tracking Rotation
        try
        {
            //Taking Data From Gyro
            string gyroValues = BluetoothService.ReadFromBluetooth();
            string[] angles   = gyroValues.Split(' ');
            
            //Executing Action
            if (angles[0].Length != 0)
            {
                float requiredAngle = float.Parse(angles[0]);

                if (requiredAngle >= 90)
                    RodThrown();

                else if (requiredAngle <=0)
                    RodPulled();
            }
        }
        catch (Exception e)
        {

        }
    }

    //Function For Testing 
    void ExecuteAction()
    {
        if(toggleAction)
            RodThrown();
        else
            RodPulled();
        
        toggleAction = !toggleAction;
    }
}
