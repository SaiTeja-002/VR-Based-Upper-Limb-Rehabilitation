using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimation : MonoBehaviour
{

    private Animator animator;

    void OnEnable()
    {
        PlayerController.PlayerDeadAction += StopAnimation;
        PlayerController.PlayerRestartAction += StartAnimation;
    }

    void OnDisable()
    {
        PlayerController.PlayerDeadAction -= StopAnimation;
        PlayerController.PlayerRestartAction += StartAnimation;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.Play("Left Diagonal 2");
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.Play("Right Diagonal 2");
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.Play("Jump");
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.Play("Dive");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            animator.Play("Slide");
        }
    }

    void StopAnimation()
    {
        // animator.Stop("Fast Run");
        animator.enabled = false;
    }

    void StartAnimation()
    {
        animator.enabled = true;
    }
}
