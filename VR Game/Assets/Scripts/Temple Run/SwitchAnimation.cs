using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimation : MonoBehaviour
{

    private Animator animator;

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

        if(Input.GetKeyDown(KeyCode.J))
        {
            animator.Play("Jump");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            animator.Play("Dive");
        }
    }
}
