using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainManager : MonoBehaviour
{
    public static Action RemoveBridgeAction = null;
    public static Action<int> NewTerrainSpawnAction = null;

    public GameObject player;

    private int count, terrainSize, alt, terrainIndex, curTerrainIndex;

    [SerializeField] private Terrain[] listOfTerrains;
    [SerializeField] private float[] yOffset;


    void OnEnable()
    {
        PlayerController.PlayerDeadAction += RelocateTerrains;
    }

    void OnDisable()
    {
        PlayerController.PlayerDeadAction -= RelocateTerrains;
    }

    void Start()
    {
        count = 1;
        terrainSize = 500;
        terrainIndex = 0;
        curTerrainIndex = terrainIndex;

        listOfTerrains[0].transform.position = new Vector3(-250, yOffset[0], 0);
        
        if(NewTerrainSpawnAction != null)
        {
            NewTerrainSpawnAction(0);
        }
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

            if(terrainIndex != 2 && NewTerrainSpawnAction != null)
            {
                NewTerrainSpawnAction((count-1)*(terrainSize/2));
            }
        }
    }

    void RelocateTerrains()
    {
        for(int i=0; i<listOfTerrains.Length; i++)
        {
            listOfTerrains[i].transform.position = new Vector3(-250, yOffset[i], -1000*i);
        }

        count = 1;
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
