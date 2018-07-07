using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLikeLightning : MonoBehaviour {

    public GameObject FidgetSpinner;
    public float SpinSpeed = 1;

	void Update () {
        transform.Rotate(0,0,SpinSpeed * Time.deltaTime);
	}
}
