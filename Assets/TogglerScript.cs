using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglerScript : MonoBehaviour
{
    public Light pointLight;
    void Start()
    {
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        pointLight.intensity = Random.Range(0.4f, 1.05f);
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(FlickerLight());
    }
}
