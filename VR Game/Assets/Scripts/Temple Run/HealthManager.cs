using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int chances;

    private Vector3 initialScale, change;
    private float decrementAmount;

    void OnEnable()
    {
        PlayerController.ObstacleCollisionAction += ScaleDownHealth;
        PlayerController.PlayerRestartAction += ScaleUpHealth;
    }

    void OnDisable()
    {
        PlayerController.ObstacleCollisionAction -= ScaleDownHealth;
        PlayerController.PlayerRestartAction -= ScaleUpHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        decrementAmount = initialScale.x/chances;

        change = new Vector3(decrementAmount, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScaleDownHealth()
    {
        transform.localScale -= change;
        Debug.Log("Scaling down the health");
    }

    void ScaleUpHealth()
    {
        transform.localScale = initialScale;
    }

}
