using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private bool turnLeft, turnRight;
    private float strafeDirection;
    public float speed = 7.0f;
    private CharacterController myCharacterController;
    private Rigidbody rb;
    // private Animator animator;

    private float jumpTime, jumpSub;
    private float diveTime, diveSub;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpSpeed;
    private bool jump;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float lanePos;
    [SerializeField] private float strafeSpeed;
    private float curX, newXPos, curY;

    // Start is called before the first frame update
    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        // animator = GetComponent<Animator>();
        
        transform.position = new Vector3(-lanePos, transform.position.y, transform.position.z);
        newXPos = -lanePos;
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        // velocity.y = Mathf.Sqrt(2f*9.81f*jumpHeight*jumpSub);

        // strafeDirection = Input.GetAxisRaw("Horizontal");

        // transform.Rotate(new Vector3(0f, strafeDirection*90f, 0f));

        // turnLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        // turnRight = Input.GetKeyDown(KeyCode.RightArrow);

        // myCharacterController.SimpleMove(new Vector3(0f, 0f, 0f));
        myCharacterController.Move(transform.forward * speed * Time.deltaTime);
        // transform.position = new Vector3(strafeDirection, transform.position.y, transform.position.z);
        // myCharacterController.Move(transform.right * strafeDirection * Time.deltaTime);
        // myCharacterController.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            newXPos = -lanePos;
            // animator.Play("Left Diagonal 1");
            // transform.position = new Vector3(-1f*lanePos, transform.position.y, transform.position.z);
            // myCharacterController.Move((newXPos - transform.position.x) * Vector3.right);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            newXPos = lanePos;
            // animator.Play("Right Diagonal 1");
            // myCharacterController.Move((newXPos - transform.position.x) * Vector3.right);
            // transform.position = new Vector3(lanePos, transform.position.y, transform.position.z);
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            // jump = true;
            // rb.AddForce(0, 10, 0);
            rb.velocity = Vector3.up * 20f;
        }
        // else
        // {
        //     jump = false;
        // }

        curX = Mathf.Lerp(curX, newXPos, Time.deltaTime*strafeSpeed);

        // if(jump)
        // {
        //     curY = Mathf.Lerp(curY, jumpHeight, Time.deltaTime*jumpSpeed);
        // }

        myCharacterController.Move((curX - transform.position.x) * Vector3.right);
        // myCharacterController.Move((curY - transform.position.y) * Vector3.Up);
    }

    /*void Update()
    {
        turnLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        turnRight = Input.GetKeyDown(KeyCode.RightArrow);

        if (turnLeft)
            transform.Rotate(new Vector3(0f, -90f, 0f));
        else if (turnRight)
            transform.Rotate(new Vector3(0f, 90f, 0f));

        myCharacterController.SimpleMove(new Vector3(0f, 0f, 0f));
        myCharacterController.Move(transform.forward * speed * Time.deltaTime);

        // if(Input.GetKeyDown(KeyCode.D))
        // {
        //     animator.SetBool("isJumping", false);
        //     animator.SetBool("isDiving", true);
        // }
        // else if(Input.GetKeyDown(KeyCode.J))
        // {
        //     animator.SetBool("isJumping", true);
        //     animator.SetBool("isDiving", false);
        // }
    }*/
}
