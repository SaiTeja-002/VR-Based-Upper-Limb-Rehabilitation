using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBridge : MonoBehaviour
{

    private int childs, prevIndex;
    [SerializeField] private int count;

    void OnEnable()
    {
        TerrainManager.RemoveBridgeAction += RandomDisable;
    }

    void OnDisable()
    {
        TerrainManager.RemoveBridgeAction -= RandomDisable;
    }

    // Start is called before the first frame update
    void Start()
    {
        childs = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomDisable()
    {
        prevIndex = 0;
        EnableAll();

        // count = Mathf.FloorToInt(childs/4);

        for(int i=0; i<count; i++)
        {
            int randomChildIndex = Random.Range(0, childs);
            transform.GetChild(randomChildIndex).gameObject.SetActive(false);

            prevIndex = i;
        }
    }

    void EnableAll()
    {
        for(int i=0; i<childs; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
