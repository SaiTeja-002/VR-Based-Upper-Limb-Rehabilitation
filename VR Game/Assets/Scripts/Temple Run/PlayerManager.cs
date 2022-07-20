using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static Action RemoveBridgeAction = null;

    public GameObject player;

    private int count, terrainSize, alt, terrainIndex, curTerrainIndex;

    [SerializeField] private Terrain[] listOfTerrains;
    [SerializeField] private float[] yOffset;


    void Start()
    {
        count = 1;
        terrainSize = 500;
        terrainIndex = 0;
        curTerrainIndex = terrainIndex;

        listOfTerrains[0].transform.position = new Vector3(-250, yOffset[0], 0);
    }

    void Update()
    {
        if(player.transform.position.z >= count*(terrainSize/2))
        {
            count += 2;

            while(terrainIndex == curTerrainIndex)
                terrainIndex = UnityEngine.Random.Range(0, listOfTerrains.Length);

            curTerrainIndex = terrainIndex;

            if(terrainIndex == 2)
            {
                if(RemoveBridgeAction != null)
                {
                    RemoveBridgeAction();
                }
            }

            listOfTerrains[terrainIndex].transform.position = new Vector3(-250, yOffset[terrainIndex], (count-1)*(terrainSize/2));
        }
    }




    // Start is called before the first frame update
    // void Start()
    // {
    //     count = 1;
    //     terrainSize = 500;
    //     alt = 1;
    //     offset1 = 0;
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(player.transform.position.z >= count*terrainSize/2)
    //     {
    //         count += 2;
    //         alt *= -1;

    //         if(alt == -1)
    //         {
    //             terrain2.transform.position = new Vector3(-250, -4.9f, (count-1)*terrainSize/2);
    //             Debug.Log("Moving Terrain 2");
    //         }
    //         else
    //         {   
    //             terrain1.transform.position = new Vector3(-250, -19.9f, (count-1)*terrainSize/2 - offset1);
    //             Debug.Log("Moving Terrain 1");
    //         }
    //     }
    // }

    // public Terrain terrain1;
    // public Terrain terrain2;

    // private Terrain curTerrain;

    // public GameObject player;


    // void Start()
    // {
    //     curTerrain = terrain1;
    // }
}
