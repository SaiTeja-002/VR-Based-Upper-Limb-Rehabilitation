using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject asteroidParent;

    public int maxNumberOfObstacles, xRange, yRange, zRange;
    public float density;
    public float minObstacleDistance, safeDistance, destroyDistance, spawnDistance;

    private Vector3 asteroidPosition;
    public GameObject[] asteroidTypes;      //Stores different asteroid types

    private List<GameObject> asteroids = new List<GameObject>();                //Used to keep track of the asteroids
    private List<GameObject> listOfAsteroidParents = new List<GameObject>();    //Used to keep track of the asteroid parents


    void OnEnable()
    {
        GameOverCanvasScript.RetryAction += Reset;
    }

    void OnDisable()
    {
        GameOverCanvasScript.RetryAction -= Reset;
    }

    void Start()
    {
        safeDistance = 3f;

        float volume = 8*(xRange)*(yRange)*(zRange);
        maxNumberOfObstacles = (int) (density*volume);

        Debug.Log("Denisty - " + density);
        Debug.Log("Vloume - " + volume);
        Debug.Log("maxNumberOfObstacles - " + maxNumberOfObstacles);

        CreateAsteroidBelt();

        // for(int i=0; i<maxNumberOfObstacles; i++)
        // {
        //     SelectAsteroidLocation();

        //     // If the asteroid is too close to the player, a new random position is chosen
        //     while(Vector3.Distance(asteroidPosition, player.transform.position) < safeDistance)
        //     {
        //         SelectAsteroidLocation();
        //     }

        //     for(int j=0; j<i; j++)
        //     {
        //         if(Vector3.Distance(asteroidPosition, asteroids[j].transform.position) < minObstacleDistance)
        //         {
        //             SelectAsteroidLocation();
        //         }
        //     }            
            
        //     asteroids.Add(Instantiate(asteroidTypes[Random.Range(0, asteroidTypes.Length)], asteroidPosition, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));

        //     transform. localScale = new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10));

        //     asteroids[i].transform.SetParent(asteroidParent.transform);
        // }

        // listOfAsteroidParents.Add(asteroidParent);

        StartCoroutine(PingDistanceCheck());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            CreateAsteroidParent();
            // CreateAsteroidBelt();
            listOfAsteroidParents.Add(Instantiate(listOfAsteroidParents[Random.Range(0, listOfAsteroidParents.Count)], player.transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
        }
    }

    void CreateAsteroidBelt()
    {
        // GameObject newAsteroidParent = new GameObject();

        for(int i=0; i<maxNumberOfObstacles; i++)
        {
            // newAsteroidParent.transform.position = player.transform.position;

            SelectAsteroidLocation();

            // If the asteroid is too close to the player, a new random position is chosen
            while(Vector3.Distance(asteroidPosition, player.transform.position) < safeDistance)
            {
                SelectAsteroidLocation();
            }

            for(int j=0; j<i; j++)
            {
                if(Vector3.Distance(asteroidPosition, asteroids[j].transform.position) < minObstacleDistance)
                {
                    SelectAsteroidLocation();
                }
            }            
            
            asteroids.Add(Instantiate(asteroidTypes[Random.Range(0, asteroidTypes.Length)], asteroidPosition, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));

            transform. localScale = new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10));

            asteroids[i].transform.SetParent(asteroidParent.transform);
        }

        listOfAsteroidParents.Add(asteroidParent);
    }

    /*A Function to choose a random position for the asteroid*/
    void SelectAsteroidLocation()
    {
        asteroidPosition = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), Random.Range(0, zRange));
    }

    /*Function that destroys the asteroid parents if its distance from the player is greater than a threshold*/
    void DistanceCheck()
    {
        for(int i=1; i<listOfAsteroidParents.Count; i++)
        {
            // If the distance between an asteroid parent and the player is greater than the threshold, the parent will be destroyed
            
            if(Vector3.Distance(player.transform.position, listOfAsteroidParents[i].transform.position) > destroyDistance)
            {
                Destroy(listOfAsteroidParents[i]);
                listOfAsteroidParents.Remove(listOfAsteroidParents[i]);
            }
        }

        if(Vector3.Distance(player.transform.position, listOfAsteroidParents[listOfAsteroidParents.Count-1].transform.position) > spawnDistance)
        {
            CreateAsteroidParent();
            // CreateAsteroidBelt();
        }
    }

    /*A Coroutine used to ping the DistanceCheck() for every 0.1 seconds*/
    public IEnumerator PingDistanceCheck()
    {
        for(;;)
        {
            DistanceCheck();

            yield return new WaitForSeconds(0.1f);
        }
    }

    void Reset()
    {
        for(int i=1; i<listOfAsteroidParents.Count; i++)
        {
            Destroy(listOfAsteroidParents[i]);
            listOfAsteroidParents.Remove(listOfAsteroidParents[i]);
        }

        // Invoke("CreateAsteroidBelt", 2f);
    }

    void CreateAsteroidParent()
    {
        listOfAsteroidParents.Add(Instantiate(listOfAsteroidParents[Random.Range(0, listOfAsteroidParents.Count)], player.transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
    
        // listOfAsteroidParents.Add(Instantiate(listOfAsteroidParents[Random.Range(0, listOfAsteroidParents.Count)], player.transform.position, player.transform.rotation));
    }

}