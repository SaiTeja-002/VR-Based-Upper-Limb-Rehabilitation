using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static Action PlayerDeadAction = null;
    public static Action PlayerRestartAction = null;
    public static Action ObstacleCollisionAction = null; 

    public float speed;

    private CharacterController characterController;
    private Rigidbody rb;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float upperThreshold;

    [SerializeField] private float jumpThreshold;
    [SerializeField] private float diveThreshold;
    [SerializeField] private float rightThreshold;
    [SerializeField] private float leftThreshold;

    [SerializeField] private float gyroHorizontalAngle;
    [SerializeField] private float gyroVerticalAngle;

    [SerializeField] private float lanePos;
    [SerializeField] private float strafeSpeed;

    private float curX, newXPos, curY, newYPos;

    private Vector3 startingPos;

    private int health;
    [SerializeField] private int chances;
    private float damage;

    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        
        transform.position = new Vector3(-lanePos, transform.position.y, transform.position.z);
        newXPos = -lanePos;

        health = 100;
        damage = health/chances;
        Debug.Log("Damage - " + damage);

        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
        {
            try
            {
                //Taking Data From Gyro
                string gyroValues = BluetoothService.ReadFromBluetooth();
                string[] angles   = gyroValues.Split(' ');
                
                //Executing Action
                float gyroVerticalAngle = float.Parse(angles[0]);
                float gyroHorizontalAngle = float.Parse(angles[2]);

                characterController.Move(transform.forward * speed * Time.deltaTime);

                if(gyroHorizontalAngle <= rightThreshold)   //Negative Angle
                {
                    newXPos = lanePos;
                }

                if(gyroHorizontalAngle >= leftThreshold)    //Positive Angle
                {
                    newXPos = -lanePos;
                }

                if(gyroVerticalAngle >= jumpThreshold)      //Positive Angle
                {
                    newYPos = jumpHeight;
                }

                // if(gyroVerticalAngle <= diveThreshold)
                // {
                //     new
                // }

                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    newXPos = -lanePos;
                }

                if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    newXPos = lanePos;
                }

                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    newYPos = jumpHeight;
                    // jumpHeight = 10f;
                    // rb.AddForce(Vector3.up * 20f);
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    newYPos = 0f;
                    // jumpHeight = 0f;
                }

                if(Input.GetKeyDown(KeyCode.J))
                {
                    // jump = true;
                    // rb.AddForce(0, 10, 0);
                    // rb.velocity = Vector3.up * 20f;
                }

                if(Input.GetKey(KeyCode.LeftShift))
                {
                    speed = 50f;
                }
                else
                {
                    speed = 7f;
                }

                curX = Mathf.Lerp(curX, newXPos, Time.deltaTime*strafeSpeed);
                curY = Mathf.Lerp(curY, newYPos, Time.deltaTime*jumpSpeed);

                if(Mathf.Abs(curY - newYPos) <= upperThreshold)
                {
                    newYPos = 0f;
                }

                characterController.Move((curX - transform.position.x) * Vector3.right);
                characterController.Move((curY - transform.position.y) * Vector3.up);
            }
            catch(Exception e)
            {
                
            }
        }

        else
        {
            transform.position = startingPos;

            if(PlayerDeadAction != null)
            {
                PlayerDeadAction();
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                if(PlayerRestartAction != null)
                {
                    PlayerRestartAction();
                    health = 100;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            if(ObstacleCollisionAction != null)
            {
                ObstacleCollisionAction();
            }

            Destroy(other);

            if(health > 0)
                health -= (int)damage;
        }
    }
}
