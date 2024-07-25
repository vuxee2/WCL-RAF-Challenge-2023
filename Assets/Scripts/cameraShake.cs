using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{  
    public static bool doShake = false;
    public static bool explosionShake = false;
    void Update()
    {
        if(doShake)
        {
            StartCoroutine(Shake(.15f,.1f));
            doShake = false;
        }
        if(explosionShake)
        {
            StartCoroutine(Shake(.5f,.4f));
            explosionShake = false;
        }
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f,1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
