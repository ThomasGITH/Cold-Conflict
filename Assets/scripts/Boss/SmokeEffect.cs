using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : MonoBehaviour {

    private int health;
    private float rate = 20;
    private ParticleSystem ps;
    
	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        health = GetComponentInParent<bossBehaviour>().health;
        var emission = ps.emission;

        if (health != GetComponentInParent<bossBehaviour>().maxHealth)
        {
            health /= 10;
            emission.rateOverTime = rate / health * 1.75f;
            var myMain = ps.main;
            myMain.startColor = new Color(myMain.startColor.color.r, myMain.startColor.color.g, myMain.startColor.color.b, rate / health);
        }
        else {
            emission.rateOverTime = 0.0f;
        }

	}
}
