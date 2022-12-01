using UnityEngine;
using System.IO;
using System.IO.Ports;
using System;
using System.Collections;
using System.Collections.Generic;

public class BluetoothReceiver : MonoBehaviour {

    // SerialPort sp;

    private float next_time, gyroAlongX, gyroAlongY, gyroAlongZ;

    private Vector3 initialPos, finalPos, finalRot;
    private Vector3 curPos, curRot;

    private int rotation;

    public float threshold = -5f;
    [SerializeField] Transform origin = null;

    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream = null;

    public static Action<bool> PouringAction = null;


    // Use this for initialization
    void Start () 
    {
        // string the_com="";
        // next_time = Time.time;
        
        // foreach (string mysps in SerialPort.GetPortNames())
        // {
        //     print(mysps);
        //     if (mysps != "COM3") { the_com = mysps; break; }
        // }
        // sp = new SerialPort("\\\\.\\" + the_com, 9600);
        // if (!sp.IsOpen)
        // {
        //     print("Opening " + the_com + ", baud 9600");
        //     sp.Open();
        //     sp.ReadTimeout = 100;
        //     sp.Handshake = Handshake.None;
        //     if (sp.IsOpen) { print("Open"); }
        // }

        initialPos = transform.position;

        curPos = initialPos;
        curRot = transform.eulerAngles;

        finalPos = initialPos;
        finalRot = transform.eulerAngles;


        rotation = 0;
    }

    // Update is called once per frame
    void Update() {
        try
        {
            // if (Time.time > next_time) 
            // { 
            //     if (!sp.IsOpen)
            //     {
            //         sp.Open();
            //         print("Reopening sp");
            //     }
            //     if (sp.IsOpen)
            //     {
            //         string gyroValues = sp.ReadLine();
                    
            //         curPos = transform.position;
            //         curRot = transform.eulerAngles;


            //         string[] angles = gyroValues.Split(' ');

            //         gyroAlongX = float.Parse(angles[0]) * Mathf.PI / 180;    // Converting into radians
            //         gyroAlongY = float.Parse(angles[2]) * Mathf.PI / 180;    
            //         gyroAlongZ = float.Parse(angles[1]);

            //         finalPos = initialPos - new Vector3(1.876f * Mathf.Tan(gyroAlongY), -1.876f * Mathf.Tan(gyroAlongX), 0);
            //         finalRot = new Vector3(0, 0, gyroAlongZ);
            //         rotation = (gyroAlongZ < 0) ?(1) : (-1);
            //         Debug.Log("X - " + gyroAlongX + "Y - " + gyroAlongY * 180 / Mathf.PI + "Z - " + gyroAlongZ);

            //         // sp.BaseStream.Flush();
            //     }
            //     next_time = Time.time + 2.1f;
            // }

            if(Time.time > next_time)
            {
                string gyroValues = BluetoothService.ReadFromBluetooth();
                
                curPos = transform.position;
                curRot = transform.eulerAngles;


                string[] angles = gyroValues.Split(' ');

                gyroAlongX = float.Parse(angles[0]) * Mathf.PI / 180;    // Converting into radians
                gyroAlongY = float.Parse(angles[2]) * Mathf.PI / 180;    
                gyroAlongZ = -float.Parse(angles[1]);

                finalPos = initialPos - new Vector3(1.876f * Mathf.Tan(gyroAlongY), -1.876f * Mathf.Tan(gyroAlongX), 0);
                finalRot = new Vector3(0, 0, gyroAlongZ);
                rotation = (gyroAlongZ < 0) ?(1) : (-1);
                Debug.Log("X - " + gyroAlongX + "Y - " + gyroAlongY * 180 / Mathf.PI + "Z - " + gyroAlongZ);
                
                next_time = Time.time + 2.1f;
            }

        }
        catch(Exception e)
        {
            
        }

        transform.rotation = Quaternion.Euler(finalRot);
        // Debug.Log("Time - " + Time.time + "next_time = " + next_time);

        float t = (next_time - Time.time)/2.1f;

        bool pourCheck = gyroAlongZ < threshold;

        if(isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if(isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
            
        if(t >= 0 && t <= 1)
        {   
            transform.position = finalPos + (-finalPos + curPos) * t;
            
            // bool condition1 = (finalRot - (finalRot - curRot) * t).z > 90;
            // bool condition2 = -90 > (finalRot - (finalRot - curRot) * t).z;

            // if(condition1 && rotation == 1)
            // {
            //     transform.rotation = Quaternion.Euler(0,0,270);
            //     rotation = 0;
            // }
            // else if (condition2 && rotation == -1)
            // {
            //     transform.rotation = Quaternion.Euler(0,0,90);
            //     rotation = 0;
            // }
            // else if (rotation != 0 && (finalRot-(finalRot-curRot)*t).z < 0)
            //     transform.rotation = Quaternion.Euler(0,0,Mathf.Abs((finalRot-(finalRot-curRot)*t).z));

            // else if (rotation != 0 && (finalRot-(finalRot-curRot)*t).z > 0)
            //     transform.rotation = Quaternion.Euler(0,0,360 - (finalRot-(finalRot-curRot)*t).z);
            
        }
    }

    private void StartPour()
    {
        Debug.Log("Start");

        if(PouringAction != null)
            PouringAction(true);

        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        Debug.Log("End");
        
        if(PouringAction != null)
            PouringAction(false);

        currentStream.End();
        currentStream = null;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);

        return streamObject.GetComponent<Stream>();
    }

    public float getGyroAngleZ()
    {
        return gyroAlongZ;
    }
}
