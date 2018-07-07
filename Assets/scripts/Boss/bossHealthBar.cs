using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealthBar : MonoBehaviour
{

    public float totalHealth, currenthealth;

    // Use this for initialization
    void Start()
    {
        totalHealth = transform.parent.gameObject.GetComponentInParent<bossBehaviour>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currenthealth = transform.parent.gameObject.GetComponentInParent<bossBehaviour>().health;
        transform.localScale = new Vector3((currenthealth / totalHealth), transform.localScale.y, transform.localScale.z);
    }
}
