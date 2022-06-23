using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private List<GameObject> listOfObstacles = new List<GameObject>();
    private GameObject obstacle;
    public int maxObstacleSpawnCount;
    private Vector3 obstaclePosition;
    private Vector3 obstacleTransform;
    public float safeDistance;
    public float minObstacleSeperation;
    private float lowerZLimit;
    public float spawnRange;
    private int choice = 1;

    // Start is called before the first frame update
    void Start()
    {
        safeDistance = 50f;
        minObstacleSeperation = 5f;
        maxObstacleSpawnCount = 10;
        spawnRange = 15;

        if(choice == 1)
            lowerZLimit = -spawnRange*safeDistance;
        else
            lowerZLimit = 0;

        for(int i=0; i<maxObstacleSpawnCount; i++)
        {
            SpawnPoint();

            while(Vector3.Distance(obstaclePosition, Vector3.zero) > safeDistance)
                for(int j=0; j<i; j++)
                    if(Vector3.Distance(listOfObstacles[j].transform.position, obstaclePosition) < minObstacleSeperation)
                        SpawnPoint();
            
            listOfObstacles.Add(Instantiate(obstacle, obstaclePosition, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))));

            listOfObstacles[i].transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPoint()
    {
        obstaclePosition.x = Random.Range(-spawnRange*safeDistance, spawnRange*safeDistance);
        obstaclePosition.y = Random.Range(-spawnRange*safeDistance, spawnRange*safeDistance);
        obstaclePosition.z = Random.Range(lowerZLimit, spawnRange*safeDistance);

        // if(obstaclePosition.magnitude > 1)
        //     obstaclePosition.Normalize();

        // obstacleTransform = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
    }
}
