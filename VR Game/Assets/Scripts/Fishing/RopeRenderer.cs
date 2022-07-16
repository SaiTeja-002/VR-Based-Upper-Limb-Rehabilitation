using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    public LineRenderer rope;

    //Rendering The Rope Constantly
    void Update()
    {
        rope.positionCount = transform.childCount;
        for(int i=0;i<rope.positionCount;i++)
            rope.SetPosition(i,transform.GetChild(i).localPosition);
    }
}
