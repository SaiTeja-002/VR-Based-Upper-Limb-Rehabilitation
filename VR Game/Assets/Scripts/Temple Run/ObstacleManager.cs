using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class ObstacleManager : MonoBehaviour
{
    public static Action ObstacleDodgedAction = null;

    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private int obstacleDistance;
    [SerializeField] private float[] listOfOffsets;
    
    public GameObject player;
    private Vector3 position;

    private List<GameObject> listOfObstacles = new List<GameObject>();
    private List<bool> crossedList = new List<bool>();
    

    void OnEnable()
    {
        TerrainManager.NewTerrainSpawnAction += GenerateObstacles;
        PlayerController.PlayerDeadAction += DestroyObstacles;
        PlayerController.PlayerRestartAction += RecreateObstacles;
    }

    void OnDisable()
    {
        TerrainManager.NewTerrainSpawnAction -= GenerateObstacles;
        PlayerController.PlayerDeadAction -= DestroyObstacles;
        PlayerController.PlayerRestartAction += RecreateObstacles;
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PingCheckIfDodged());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateObstacles(int startingPos)
    {
        for(int i=startingPos+obstacleDistance; i<=startingPos+500; i+=obstacleDistance)
        {
            int randomIndex = UnityEngine.Random.Range(0, obstaclePrefabs.Length);

            float rand = UnityEngine.Random.Range(0f, 2f);

            if(rand < 1f)
                position = new Vector3(listOfOffsets[2*randomIndex], 0, i);
            else
                position = new Vector3(listOfOffsets[2*randomIndex+1], 0, i);

            listOfObstacles.Add(Instantiate(obstaclePrefabs[randomIndex], position, Quaternion.identity));
            crossedList.Add(false);
        }
    }

    void DestroyObstacles()
    {
        for(int i=0; i<listOfObstacles.Count; i++)
        {
            Destroy(listOfObstacles[i]);
            listOfObstacles.Remove(listOfObstacles[i]);
            crossedList[i] = false;
        }
    }

    void RecreateObstacles()
    {
        GenerateObstacles(0);
    }

    void CheckIfDodged()
    {
        for(int i=0; i<listOfObstacles.Count; i++)
        {
            if(player.transform.position.z >= listOfObstacles[i].transform.position.z)
            {
                if(!crossedList[i])
                {
                    crossedList[i] = true;

                    if(ObstacleDodgedAction != null)
                    {
                        ObstacleDodgedAction();
                    }
                }
            }
        }
    }

    public IEnumerator PingCheckIfDodged()
    {
        while(true)
        {
            CheckIfDodged();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
