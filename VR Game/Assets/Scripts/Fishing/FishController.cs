using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishController : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem bubbles;
    public GameObject fish;

    public float fishSwimSpeed;
    public float fishSinkSpeed;
    public float fishFloatSpeed;
    public float shrinkSpeed;

    private bool reachedSurface;
    private bool isCaught;

    public static Action FishCaught = null;

    // Enabling Particles + Fish Settings
    void Start()
    {
        //Switching on Particles
        bubbles.Play();

        //Heading Direction
        if(transform.position.x <= 12.8)
            transform.rotation  = Quaternion.Euler(0,-90,0);
        else
            transform.rotation  = Quaternion.Euler(0,+90,0);

        //Surface Status
        reachedSurface = false;

        //Fish Still Not Caught
        isCaught = false;
    }

    // Moves The Fish Around
    void Update()
    {
        if(isCaught) return ;

        //After Reaching The Surface
        if(reachedSurface)
            transform.position -= transform.forward*fishSwimSpeed*Time.deltaTime;

        //Swimming Upto Top
        else if (transform.position.y < -10.3f)
            transform.position += transform.up*fishFloatSpeed*Time.deltaTime;
        
        //Reached Surface
        else
            reachedSurface = true;
    }

    //Fish Got Baited
    void OnTriggerEnter(Collider other)
    {
        //Adjusting Position & Scale
        if(other.transform.tag == "Hook")
        {
            bubbles.Stop();
            isCaught = true;
            StartCoroutine(TakeTheBait());
            transform.position = other.transform.position;
        }
    }

   IEnumerator TakeTheBait()
   {
        //Size Limits
        Vector3 maxSize = transform.localScale;
        Vector3 minSize = new Vector3(0.5f,0.5f,0.5f);

        //Shrinking Effect
        while(transform.localScale.x > 0.63)
        {
            transform.localScale = Vector3.Lerp(minSize,maxSize,transform.position.z/50.5f);
            yield return null;
        }

        //Fish Despawn
        float countDown = 2.0f;
        while(countDown > 0)
        {
            countDown -= Time.deltaTime;
            yield return null;
        }
        
        FishCaught();
        transform.gameObject.SetActive(false);
   }
}
