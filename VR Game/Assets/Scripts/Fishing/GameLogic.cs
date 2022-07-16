using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject fishPrefab;
    public float lifeTime;

    private GameObject fish;
    private float fishAge;

    //Spawning The First Fish
    void Start()
    {
        SpawnFish();
    }

    //Spawns & Moves The Fishes
    void Update()
    {
        if(fishAge <= 0.0f)
        {
            Destroy(fish);
            SpawnFish();
        }

        fishAge -= Time.deltaTime;
    }

    //Spawns A Fish in Random Position of Lake
    void SpawnFish()
    {
        Vector3 fishPosition = new Vector3(Random.Range(9.7870014f,15.8129986f),-18f,50.5f);
        fish = Instantiate(fishPrefab,fishPosition, Quaternion.identity);
        fishAge = lifeTime;
    }
}
