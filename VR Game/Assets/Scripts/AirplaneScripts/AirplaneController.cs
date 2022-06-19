using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{

    public float upwardRotationFactor = 1.0f;
    public float sidewardRotationFactor = 1.0f;
    public float thrust = 2.0f;

    private int alt;
    private int dir;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Up key is held pressed");
            alt = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Down key is held pressed");
            alt = -1;
        }
        else
            alt = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Left key is held pressed");
            dir = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Right key is held pressed");
            dir = 1;
        }
        else
            dir = 0;

        if(alt != 0)
            UpDown();
        if(dir != 0)
            LeftRight();
    }

    void UpDown()
    {

    }

    void LeftRight()
    {

    }
}
