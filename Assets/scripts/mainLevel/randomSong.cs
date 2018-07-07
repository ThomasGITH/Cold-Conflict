using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSong : MonoBehaviour {


    public AudioSource soundTrack;
    public AudioClip song1, song2, song3, song4;

	// Use this for initialization
	void Start () {

        Screen.SetResolution(1920, 1080, true);

        soundTrack = GetComponent<AudioSource>();
        chooseSoundtrack();
	}
	
	// Update is called once per frame
	void Update () {
        if (!soundTrack.isPlaying)
        {
            chooseSoundtrack();
        }

	}

    void chooseSoundtrack()
    {
        int randomNumber = Random.Range(0, 100);

        if (randomNumber <= 25)
        {
            soundTrack.clip = song1;
            soundTrack.pitch = 1;
        }
        else if ((randomNumber > 25)&&(randomNumber <= 50))
        {
            soundTrack.clip = song2;
            soundTrack.pitch = 1;
        }
        else if ((randomNumber > 50) && (randomNumber <= 75))
        {
            soundTrack.clip = song3;
            soundTrack.pitch = 1.25f;
        }
        else if ((randomNumber > 75) && (randomNumber <= 100))
        {
            soundTrack.clip = song4;
            soundTrack.pitch = 1;
        }

        soundTrack.Play();
    }
}
