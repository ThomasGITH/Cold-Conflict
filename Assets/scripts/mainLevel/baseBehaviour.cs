using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseBehaviour : MonoBehaviour {

    public int health = 1500, totalHealth;
    public Sprite base1, base2, base3, base4, base5,base6,base7;
    private SpriteRenderer spriteRenderer;
    private int damage = 50, baseID = 0, spriteSwitch = 1500;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        totalHealth = health;
        spriteSwitch = totalHealth;
        damage = totalHealth / 7;
    }

    // Update is called once per frame
    void Update () {
        Sprite[] fields = { base1, base2, base3, base4, base5, base6,base7 };

        if(health <= spriteSwitch - damage)
        {
            spriteSwitch -= damage;
            baseID += 1;
            if(spriteRenderer.sprite != base7)
            {
                spriteRenderer.sprite = fields[baseID];
            }
        }

        if (health < 0)
        {
            health = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("enemyBullet"))
        {
            health -= 10;
            if(health < 0)
            {
                health = 0;
            }
            Destroy(collision.gameObject);
        }else if (collision.tag == ("bullet"))
        {
            Destroy(collision.gameObject);
        }else if (collision.tag == ("bossBullet"))
        {
            health -= 12;
            if (health < 0)
            {
                health = 0;
            }
        }
    }
}
