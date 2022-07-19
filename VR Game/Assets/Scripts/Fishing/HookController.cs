using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HookController : MonoBehaviour
{
    public float throwSpeed;
    public float throwScale;

    public float pullSpeed;
    public float accuracy;

    public Transform hookeEnd;

    private bool isRetracting;
    private bool isThrowing;
    private bool isThrown;
    private bool canFloat;
    
    private bool positionSet;
    public Vector3 floatPosition;

    void Start()
    {
        isRetracting = false;
        isThrowing = false;
        isThrown = false;
        canFloat = false;
        positionSet = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        //Hooke Floating Now
        if (positionSet && canFloat)
            transform.position = floatPosition;

        if (isRetracting)
        {
           Vector3 decisionVector = transform.position-hookeEnd.position;
           float decisionNumber = decisionVector.magnitude;
           
           //Hooke Got Hooked
           if (decisionNumber <= accuracy)
           {    
                isRetracting = false;
                transform.position = hookeEnd.position;
                transform.rotation = hookeEnd.rotation;
                GetComponent<Rigidbody>().isKinematic = true;
           }
        }

        //Fishing Action List
        if(isRetracting)
        {   
            Vector3 pullDirection = hookeEnd.position-transform.position;
            transform.position += (pullDirection.normalized)*pullSpeed;
        }   

        else if(isThrowing && !isThrown)
        {
            Rigidbody objectBody = GetComponent<Rigidbody>();
            objectBody.velocity = throwSpeed*(transform.up-transform.right);
            objectBody.useGravity = true;
            isThrown = true;
        }
    }

    //Throws The Hooke
    void Throw()
    {
        if(!isThrowing)
        {
            isThrowing = true;
            isThrown = false;
            canFloat = true;
            positionSet = false;
            GetComponent<Rigidbody>().isKinematic = false;
            SetThrowSpeed();
            StartCoroutine(Float());
        }
    }

    //Retracts The Hooke
    void Retract()
    {
        if(!isRetracting)
        {
            isRetracting = true;
            canFloat = false;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    //Makes The Hooke Float
    IEnumerator Float()
    {
        while(transform.position.y > -10.3f)
        {
            yield return null;
        }

        //Throwing Done : Floating Begin
        isThrowing = false;

        //Storing Position
        positionSet = true;
        floatPosition = transform.position;

        //Floating Action
        Rigidbody hookBody   = GetComponent<Rigidbody>();
        hookBody.useGravity  = false;
        hookBody.isKinematic = true;
    }

    //Varies Throw Speed Based on Orientation
    void SetThrowSpeed()
    {
        throwSpeed = 14.3f + Mathf.Abs(throwScale*Mathf.Sin(Camera.main.transform.rotation.y*Mathf.Deg2Rad));
    }
    
    //Fish Got Hooked
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Fish")
            other.transform.parent = transform;
    }

    //Subscribing Events
    void OnEnable()
    {
        FishingEvents.RodThrown += Throw;
        FishingEvents.RodPulled += Retract;
    }

    // UnSubscribing Events
    void OnDisable()
    {
        FishingEvents.RodThrown -= Throw;
        FishingEvents.RodPulled -= Retract;
    }
}
