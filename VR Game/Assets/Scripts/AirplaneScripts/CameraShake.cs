using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float magnitude, float duration)
    {
        float timeElapsed = 0.0f, xRange = 1f, yRange = 1f, zRange = 1f;

        Vector3 originalPosition = transform.localPosition;

        while(timeElapsed <= duration)
        {
            float x = Random.Range(-xRange, xRange) * magnitude;
            float y = Random.Range(-yRange, yRange) * magnitude;
            // float z = Random.Range(-zRange, zRange) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
