using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMotion : MonoBehaviour
{
    public float spinSpeed;
    public float jumpSpeed;
    public float timeCount;
    public float timeLimit;

    // Default Spin & Jump Parameters
    void Start()
    {
        // spinSpeed = 10.0f;
        // jumpSpeed = 0.03f;
        // timeLimit = 2.00f;
        timeCount = timeLimit;
    }

    // Spins & Bounces The Cube
    void Update()
    {
        transform.Rotate(0,spinSpeed*Time.deltaTime,0); 

        if(timeCount >= 0 && timeCount <= timeLimit)
        {
            transform.Translate(Vector3.up*Time.deltaTime*jumpSpeed);
            timeCount -= Time.deltaTime;
        }

        else if (timeCount >= -timeLimit)
        {
            transform.Translate(Vector3.down*Time.deltaTime*jumpSpeed);
            timeCount -= Time.deltaTime;
        }

        else
            timeCount = timeLimit;
    }
}
