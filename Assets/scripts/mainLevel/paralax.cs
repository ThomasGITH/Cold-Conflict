using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour {

    [System.Serializable]
    public class Laag
    {
        public GameObject laag;
        public float cameraVolg;

    }

    public Laag[] lagen;
    public Transform camera;

	// Use this for initialization

	// Update is called once per frame
	void Update () {

		for (int i = 0; i < lagen.Length; i++)
        {
            lagen[i].laag.transform.position = new Vector3(camera.position.x * lagen[i].cameraVolg, camera.position.y * lagen[i].cameraVolg, lagen[i].laag.transform.position.z);

        }
	}
}
