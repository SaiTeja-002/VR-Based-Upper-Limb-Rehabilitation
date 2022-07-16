using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float xRot, yRot, zRot;

    // Start is called before the first frame update
    void Start()
    {
        xRot = Random.Range(-20, 20);
        yRot = Random.Range(-20, 20);
        zRot = Random.Range(-20, 20);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRot * Time.deltaTime, yRot * Time.deltaTime, zRot * Time.deltaTime);
    }
}
