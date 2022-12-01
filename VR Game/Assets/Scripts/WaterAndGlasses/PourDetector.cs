using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PourDetector : MonoBehaviour
{
    public float threshold = -5f;
    [SerializeField] Transform origin = null;

    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream = null;

    private float gyroAlongX, gyroAlongY, gyroAlongZ;


    public static Action<bool> PouringAction = null;

    private BluetoothReceiver btrec;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            gyroAlongZ = btrec.getGyroAngleZ(); 
        }

        catch(Exception e)
        {
            // Debug.Log("An error has occurred");
        }

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

    // private float CalculatePourAngle()
    // {
    //     float z = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).z;
    //     return z;
    // }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);

        return streamObject.GetComponent<Stream>();
    }
}
