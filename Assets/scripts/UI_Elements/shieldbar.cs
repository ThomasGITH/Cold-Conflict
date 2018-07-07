using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldbar : MonoBehaviour
{

    public Sprite shield4, shield3, shield2, shield1, shield0;

    private SpriteRenderer spriteRenderer;
    public GameObject player;
    private int shield;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Sprite[] fields = { shield0, shield1, shield2, shield3, shield4};

        shield = player.gameObject.GetComponent<movement>().shieldHealth;

        if (shield != 0)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            spriteRenderer.sprite = fields[shield];
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}