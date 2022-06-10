using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    public GameObject cube;
    public Vector3 change;

    private Vector3 initialValues;
    private bool loading = false;

    // Start is called before the first frame update
    void Start()
    {
        initialValues.x = 0;
        initialValues.y = 1.0f;
        initialValues.z = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if(loading)
            cube.transform.localScale = cube.transform.localScale + change*Time.deltaTime;

        else
            cube.transform.localScale = initialValues;
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

    void Change()
    {
        loading = true;
    }

    void Reset()
    {
        loading = false;
    }
}
