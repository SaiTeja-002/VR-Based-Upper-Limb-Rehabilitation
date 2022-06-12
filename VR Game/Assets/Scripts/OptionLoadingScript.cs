using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OptionLoadingScript : MonoBehaviour
{
    private Vector3 initialScale;
    private bool loading = false;
    public float loadBarLength;
    public float loadBarTime;
    private Vector3 change;


    void Start()
    {
        // Initial scale of the empty object
        initialScale = new Vector3(1.0f,1.0f,1.0f);
        change = new Vector3(loadBarLength/loadBarTime,0.0f,0.0f);
    }

    void Update()
    {
        // If the option is being gazed, then the empty object is scaled along the X-axis
        if(loading)
            gameObject.transform.localScale += change*Time.deltaTime;

        // Else it is set to its initial scale
        else
            gameObject.transform.localScale = initialScale;
    }

    void OnEnable()
    {
        OptionHandler.IsGazingAction += Change;
        OptionHandler.IsNotGazingAction += Reset;
    }

    void OnDisable()
    {
        OptionHandler.IsGazingAction -= Change;
        OptionHandler.IsNotGazingAction -= Reset;
    }

    /* The option is being gazed */
    void Change()
    {
        loading = true;
    }

    /* If the option is not being gazed */
    void Reset()
    {
        loading = false;
    }
}