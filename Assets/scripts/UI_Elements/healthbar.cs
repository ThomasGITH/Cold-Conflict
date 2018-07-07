using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour {

    public Sprite health10, health9, health8, health7, health6, health5, health4, health3, health2, health1, health0;

    private SpriteRenderer spriteRenderer;
    public GameObject player;
    private int health;
    List<Sprite> objects = new List<Sprite>();

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        Sprite[] fields = { health0, health1, health2, health3, health4, health5, health6, health7, health8, health9, health10 };

        health = player.gameObject.GetComponent<movement>().health;
        health /= 10;

        spriteRenderer.sprite = fields[health];
    }
}