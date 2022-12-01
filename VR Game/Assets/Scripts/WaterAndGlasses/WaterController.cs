using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterController : MonoBehaviour
{

    private bool collideWithTop = false;
    private bool isPouring = false;

    private int nozzleIndex = 0;
    public GameObject nozzle = null;

    void OnEnable()
    {
        Stream.OnSaucerHitAction += IncreaseWaterLevel;
        BluetoothReceiver.PouringAction += TogglePouring;
        // PourDetector.PouringAction += TogglePouring;
    }

    void OnDisable()
    {
        Stream.OnSaucerHitAction -= IncreaseWaterLevel;
        BluetoothReceiver.PouringAction -= TogglePouring;
        // PourDetector.PouringAction -= TogglePouring;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == nozzle.name)
        {
            collideWithTop = true;
        }
    }

    private void IncreaseWaterLevel(string name)
    {
        if(name == gameObject.name)
        {
            // Debug.Log("Calling the coroutine");
            StartCoroutine(IncreaseWaterLevelCoroutine());
        }
    }

    private IEnumerator IncreaseWaterLevelCoroutine()
    {
        while(!collideWithTop && isPouring)
        {
            // Debug.Log("Water Rising");
            
            float scaleVar = 0.00005f;
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y + scaleVar, gameObject.transform.localScale.z);

            yield return null;
        }
    }

    private void TogglePouring(bool truthValue)
    {
        isPouring = truthValue;
    }

}
