using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JugController : MonoBehaviour
{
    private float gyroAlongX, gyroAlongY, gyroAlongZ;

    private Vector3 initialPos;
    private Vector3 finalPos,finalRot;
    private Vector3 curPos,curRot;
    private float readTime;


    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;

        finalPos = initialPos;
        finalRot = transform.eulerAngles;

        curPos = initialPos;
        curRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            //Taking Data From Gyro
            string gyroValues = BluetoothService.ReadFromBluetooth();
            
            readTime = Time.time;
            curPos  = transform.position;
            curRot = transform.eulerAngles;

            string[] angles   = gyroValues.Split(' ');
            
            //Executing Action
            gyroAlongX = float.Parse(angles[0]) * Mathf.PI / 180;    
            gyroAlongY = float.Parse(angles[2]) * Mathf.PI / 180;    
            gyroAlongZ = float.Parse(angles[1]) * Mathf.PI / 180; 

            finalPos = initialPos - new Vector3(1.876f * Mathf.Tan(gyroAlongY), -1.876f * Mathf.Tan(gyroAlongX), 0);
            finalRot = new Vector3(0, 0, -gyroAlongZ*180/Mathf.PI);
        }
        catch(Exception e)
        {
            
        }
        // rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        float t = (Time.time-readTime)/3;
            
        if(t >= 0 && t <= 1)
        {
            transform.position = Vector3.Lerp(curPos,finalPos,t);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(curRot,finalRot,t));
        }

    }
}
