using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroy : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        print(GetComponent<Renderer>().material.color.a);
        if(GetComponent<Renderer>().material.color.a <= 0.01f)
        {
            Destroy(gameObject);
        }
	}
}
