using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Liquid : MonoBehaviour
{
    public GameObject mLiquid;
    public GameObject mLiquidMesh;

    public int mSloshSpeed = 60;
    public int mRotateSpeed = 15;

    public int difference = 25;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Slosh();

        mLiquidMesh.transform.Rotate(Vector3.down * mRotateSpeed * Time.deltaTime, Space.Self);
    }

    private void Slosh()
    {
        Quaternion inverseRotation = Quaternion.Inverse(transform.localRotation);

        Vector3 finalRotation = Quaternion.RotateTowards(mLiquid.transform.localRotation, inverseRotation, mSloshSpeed * Time.deltaTime).eulerAngles;

        finalRotation.x = ClampRotationValue(finalRotation.x, difference);
        finalRotation.z = ClampRotationValue(finalRotation.z, difference);

        mLiquid.transform.localEulerAngles = finalRotation;
    }

    private void MoreSlosh()
    {

    }

    private float ClampRotationValue(float value, float difference)
    {
        float returnValue = 0f;

        if(value > 180f)
            returnValue = Mathf.Clamp(value, 360 - difference, 360);
        else
            returnValue = Mathf.Clamp(value, 0, difference);

        return returnValue;
    }
}
