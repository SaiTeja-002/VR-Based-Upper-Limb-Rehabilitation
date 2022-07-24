using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirplaneController : MonoBehaviour
{

    // public ParticleSystems[] particleSystems;

    // [SerializeField]
    // private ParticleSystem sys;
    
    // [SerializeField]
    // private ParticleSystem sys2;

    // private ParticleSystem.MainModule sysMain, sysMain2;


    public float forwardSpeed, strafeSpeed, hoverSpeed;
    private float activeForwardSpeed, activeStrifeSpeed, activeHoverSpeed;
    private float forwardAcceleration, strafeAcceleration, hoverAcceleration;
    
    public float lookRotateSpeed;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed, rollAcceleration, rollSensitivity;

    [SerializeField] private float maxVerticalThreshold;
    [SerializeField] private float minVerticalThreshold;
    [SerializeField] private float maxStrafeThreshold;
    [SerializeField] private float minStrafeThreshold;
    [SerializeField] private float strafeRotationAngle;
    [SerializeField] private float verticalRotationAngle;

    private float xAngle, yAngle, zAngle;
    private int shift;

    private int moving = 1;

    private void OnEnable()
    {
        GameOverCanvasScript.RetryAction += ResetPosition;
    }

    private void OnDisable()
    {
        GameOverCanvasScript.RetryAction -= ResetPosition;
    }

    void Start()
    {
        SetParameters();

        // screenCenter.x = Screen.width * 0.5f;
        // screenCenter.y = Screen.height * 0.5f;

        // forwardSpeed = 25f;
        // strafeSpeed = 14f;
        // hoverSpeed = 5f;

        // forwardAcceleration = 2.5f;
        // strafeAcceleration = 2f; 
        // hoverAcceleration = 2f;

        // lookRotateSpeed = 90f;
        
        // rollSpeed = 4f;
        // rollAcceleration = 3.5f;
        // rollSensitivity = 6f;

        // shift = 1;

        // Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {

        if(moving == 1)
        {
            try
            {
                //Taking Data From Gyro
                string gyroValues = BluetoothService.ReadFromBluetooth();
                string[] angles   = gyroValues.Split(' ');
                
                //Executing Action
                float verticalAngle = float.Parse(angles[0]);
                float strafeAngle = float.Parse(angles[1]);

                // float verticalAngle = 0f;
                // float strafeAngle = 0f;
                

                //Forward
                activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, forwardSpeed, forwardAcceleration);
                transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;

                //Sideways
                // transform.Rotate(-verticalAngle * lookRotateSpeed * Time.deltaTime, -strafeAngle * lookRotateSpeed * Time.deltaTime, -rollSensitivity * mouseDistance.x * Time.deltaTime * shift, Space.Self);

                if(verticalAngle >= maxVerticalThreshold || Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Rotate(-verticalRotationAngle * Time.deltaTime, 0f, 0f);
                }

                if(strafeAngle >= maxStrafeThreshold || Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Rotate(0f, strafeRotationAngle * Time.deltaTime, 0f);
                }

                if(strafeAngle <= minStrafeThreshold || Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Rotate(0f, -strafeRotationAngle * Time.deltaTime, 0f);
                }
    
                // if(verticalAngle >= maxVerticalThreshold || verticalAngle <= minVerticalThreshold)
                // {
                //     transform.Rotate(-verticalRotationAngle * Time.deltaTime, 0f, 0f);
                // }

                // if(strafeAngle >= maxStrafeThreshold || strafeAngle <= minStrafeThreshold)
                // {
                //     transform.Rotate(0f, 0f, -strafeRotationAngle * Time.deltaTime);
                // }
            }
            catch (Exception e)
            {

            }

            if(Input.GetKey(KeyCode.LeftShift))
            {
                shift = 20;
            }
            else
            {
                shift = 15;
            }

            // xAngle = transform.rotation.eulerAngles.x;
            // yAngle = transform.rotation.eulerAngles.y;
            // zAngle = transform.rotation.eulerAngles.z;

            // lookInput.x = Input.mousePosition.x;
            // lookInput.y = Input.mousePosition.y;

            // mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
            // mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

            // mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

            // rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
         
            // transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, -rollSensitivity * mouseDistance.x * Time.deltaTime * shift, Space.Self);
            
            // activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, forwardSpeed, forwardAcceleration);
            // // activeStrifeSpeed = Mathf.Lerp(activeStrifeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration);
            // // activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration);

            // transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
            // // transform.position += transform.right * activeStrifeSpeed * Time.deltaTime;
            // // transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(0,0,0);
        }
    }

    private void IncreaseLevel()
    {

    }

    void ResetPosition()
    {
        transform.position = new Vector3(0, 0, 0);

        forwardSpeed = 0f;
        strafeSpeed = 0f;
        hoverSpeed = 0f;

        forwardAcceleration = 0f;
        strafeAcceleration = 0f; 
        hoverAcceleration = 0f;

        lookRotateSpeed = 0f;
        
        rollSpeed = 0f;
        rollAcceleration = 0f;
        rollSensitivity = 0f;

        shift = 0;

        moving = 0;

        transform.rotation = Quaternion.Euler(0, 0, 0);

        Invoke("SetParameters", 3f);
    }

    void SetParameters()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        forwardSpeed = 25f;
        strafeSpeed = 14f;
        hoverSpeed = 5f;

        forwardAcceleration = 2.5f;
        strafeAcceleration = 2f; 
        hoverAcceleration = 2f;

        lookRotateSpeed = 90f;
        
        rollSpeed = 4f;
        rollAcceleration = 3.5f;
        rollSensitivity = 6f;

        shift = 1;

        moving = 1;
    }

}