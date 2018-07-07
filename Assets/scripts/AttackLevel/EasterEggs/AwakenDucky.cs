using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenDucky : MonoBehaviour {

    public GameObject Ducky;
    public GameObject background;
    private AudioSource backGroundSound;

    private void Start()
    {
        backGroundSound = background.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            backGroundSound.Stop();
            background.GetComponent<randomSong>().enabled = false;
            Ducky.SetActive(true);
        }
    }
}
