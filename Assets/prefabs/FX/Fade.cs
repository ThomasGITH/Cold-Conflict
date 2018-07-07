using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine("FadeAway");
    }

    IEnumerator FadeAway()
    {
        for (float i = 1f; i > 0; i -= 0.01f)
        {
            Color smokeColor = GetComponent<Renderer>().material.color;

            smokeColor.a = i;
            GetComponent<Renderer>().material.color = smokeColor;
            yield return null;
        }
    }
}
