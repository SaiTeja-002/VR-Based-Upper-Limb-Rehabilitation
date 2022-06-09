using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OptionSpotLight : MonoBehaviour
{
    public GameObject spotLight;    //SpotLight object

    /* Subscribing to the event OnEnable */
    void OnEnable()
    {
        DirectionalLightMotion.LightOnAction += SpotLightOff;
        DirectionalLightMotion.LightOffAction += SpotLightOn;
    }

    /* Unsubscribing to the event OnDisable */
    void OnDisable()
    {
        DirectionalLightMotion.LightOnAction -= SpotLightOff;
        DirectionalLightMotion.LightOffAction -= SpotLightOn;
    }

    /* Function used to set the SpotLight active */
    void SpotLightOn()
    {
        spotLight.SetActive(true);
    }

    /* Function used to set the SpotLight inactive */
    void SpotLightOff()
    {
        spotLight.SetActive(false);
    }
}
