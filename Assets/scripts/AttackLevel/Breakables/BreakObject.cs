using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour {

    public int health = 100, totalHealth;
    public Sprite base1, base2, base3, base4, base5, base6, base7, base8;
    private SpriteRenderer spriteRenderer;
    private int damage, baseID = 0, spriteSwitch;
    public bool isAnimated = false;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        totalHealth = health;
        spriteSwitch = totalHealth;
        damage = totalHealth / 7;
    }

    // Update is called once per frame
    void Update()
    {
        Sprite[] fields = { base1, base2, base3, base4, base5, base6, base7, base8};

        if (health <= spriteSwitch - damage)
        {
            spriteSwitch -= damage;
            baseID += 1;
            if (spriteRenderer.sprite != base7)
            {
                spriteRenderer.sprite = fields[baseID];
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("bullet"))
        {
            Destroy(collision);
            health -= 10;
            if (health < 0)
            {
                health = 0;
                Destroy(gameObject);
            }

            if(health == 70 && isAnimated)
            {
                GetComponent<Animator>().enabled = false;
            }
            
        }
        
    }
}
