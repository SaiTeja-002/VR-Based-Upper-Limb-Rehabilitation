using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChildAirplaneController : MonoBehaviour
{

    public static Action AsteroidCollissionAction = null;
    private int count = 0;

    void OnCollisionEnter (Collision other) 
    {
        if (other.transform.tag == "Asteroid")
        {   
            Debug.Log("Collided with an asteroid" + count);
            count += 1;

            if(AsteroidCollissionAction != null)
            {
                AsteroidCollissionAction();
            }
        }
    }
}
