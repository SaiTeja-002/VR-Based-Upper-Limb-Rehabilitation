using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BluetoothService.CreateBluetoothObject();
        BluetoothService.StartBluetoothConnection("HC-05");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
