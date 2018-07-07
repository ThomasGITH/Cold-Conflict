using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseHealthbar : MonoBehaviour {

    public GameObject home_base;
    //baseBehaviour base_code = new baseBehaviour();
    public float totalHealth, currenthealth;

	// Use this for initialization
	void Start () {
        totalHealth = home_base.GetComponent<baseBehaviour>().health;
    }
	
	// Update is called once per frame
	void Update () {
        currenthealth = home_base.GetComponent<baseBehaviour>().health;
            transform.localScale = new Vector3((currenthealth / totalHealth), transform.localScale.y, transform.localScale.z);
    }
}
