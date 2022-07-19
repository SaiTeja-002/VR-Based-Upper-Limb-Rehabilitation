using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public GameObject player;

    public float xOffset, yOffset, zOffset;

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z + zOffset);
        transform.position = new Vector3(1f, 1f, 1f);
    }
}
