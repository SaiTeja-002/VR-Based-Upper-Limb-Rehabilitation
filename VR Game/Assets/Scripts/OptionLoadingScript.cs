using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OptionLoadingScript : MonoBehaviour
{
    public Vector3 change;
    private Vector3 initialScale;
    private bool loading = false;

    void Start()
    {
        // Initial scale of the empty object
        initialScale.x = 1.0f;
        initialScale.y = 1.0f;
        initialScale.z = 1.0f;
    }

    void Update()
    {
        // If the option is being gazed, then the empty object is scaled along the X-axis
        if(loading)
            gameObject.transform.localScale = gameObject.transform.localScale + change*Time.deltaTime;

        // Else it is set to its initial scale
        else
            gameObject.transform.localScale = initialScale;
    }

    void OnEnable()
    {
        OptionHandler.IsGazingAction += Change;
        OptionHandler.IsNotGazingAction += Reset;
        // OptionHandler.IsGazingAction += Toggle;
        // OptionHandler.IsNotGazingAction += Toggle;
    }

    void OnDisable()
    {
        OptionHandler.IsGazingAction -= Change;
        OptionHandler.IsNotGazingAction -= Reset;
        // OptionHandler.IsGazingAction += Toggle;
        // OptionHandler.IsNotGazingAction += Toggle;
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
    
    // void Toggle()
    // {
    //     loading = !loading;
    // }
}