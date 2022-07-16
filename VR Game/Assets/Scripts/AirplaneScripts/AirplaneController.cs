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

    private float xAngle, yAngle, zAngle;
    private int shift;

    private int moving = 1;

    private void OnEnable()
    {
        // sysMain = sys.main;
        // sysMain2 = sys2.main;

        // ChildAirplaneController.AsteroidCollissionAction += AsteroidCollission;

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
            if(Input.GetKey(KeyCode.LeftShift))
            {
                shift = 20;
            }
            else
            {
                shift = 15;
            }

            xAngle = transform.rotation.eulerAngles.x;
            yAngle = transform.rotation.eulerAngles.y;
            zAngle = transform.rotation.eulerAngles.z;

            lookInput.x = Input.mousePosition.x;
            lookInput.y = Input.mousePosition.y;

            mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
            mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

            mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

            rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

            // transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * mouseDistance.x, Space.Self);
            
            transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, -rollSensitivity * mouseDistance.x * Time.deltaTime * shift, Space.Self);

            // Debug.Log(mouseDistance.x);

            // if(mouseDistance.x <= 0.045f && mouseDistance.x >= -0.045f)
            //     transform.rotation = Quaternion.Euler(xAngle, mouseDistance.x * lookRotateSpeed * Time.deltaTime, 0);

            // activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration);
            
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, forwardSpeed, forwardAcceleration);
            // activeStrifeSpeed = Mathf.Lerp(activeStrifeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration);
            // activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration);

            // transform.Rotate()
            transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
            // transform.position += transform.right * activeStrifeSpeed * Time.deltaTime;
            // transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
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