using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveCounter : MonoBehaviour {

    public GameObject wave_system;
    private Animator animator;
    public AnimationClip one, two, three, four , five , six, seven , eight , nine , ten;
    private Animation animation;
    private int number_of_wave = 0;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        AnimationClip[] animationList = { one, two, three, four, five, six, seven, eight, nine, ten };
        number_of_wave = wave_system.GetComponent<enemySpawn>().number_of_wave + 1;
        //animation.clip = animationList[number_of_wave];

        animator.SetInteger("waveNumber", number_of_wave);

        if (!animation.isPlaying)
        {
         //   animation.Play();
        }
	}
}
