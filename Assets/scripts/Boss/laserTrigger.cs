using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserTrigger : MonoBehaviour {

    public GameObject home_base, player;

    private void Update()
    {
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        player = playerList[0];
        GameObject[] baseList = GameObject.FindGameObjectsWithTag("base");
        home_base = baseList[0];
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "base")
        {
            home_base.GetComponent<baseBehaviour>().health -= 3;
        }
    }
}
