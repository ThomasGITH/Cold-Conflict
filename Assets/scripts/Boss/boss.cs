using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour {

    public GameObject player, home_base;
    SpriteRenderer spriteRenderer;
    int focusPoint, switchTimer = 900, bossJumpPower = 650;
    private float moveX, moveSpeed = 3, myWidth;
    private bool firstJump = true, isGrounded = false, hasJumped = true;

    // Use this for initialization
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        focusPoint = Random.Range(0, 10);
        myWidth = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void Update() {

        if (focusPoint > 7) {
            AttackPlayer();
        }
        else
        {
            AttackBase();
        }
    }

    public void AttackPlayer()
    {
        //FLIP SPRITE
        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else { spriteRenderer.flipX = false; }
    }

    public void AttackBase()
    {
        int baseHealth = home_base.GetComponent<baseBehaviour>().health;
        if (baseHealth > 0)
        {
            switchTimer -= 1;

            if (switchTimer > 300)
            {
                walk(-1);
            }
            else if (switchTimer < 300)
            {
                walk(0.0f);
            }

            if (switchTimer <= 0)
            {
                switchTimer = 900;
            }

            Vector2 lineCastPos = transform.position - transform.right * myWidth;
            Debug.DrawLine(lineCastPos, lineCastPos + new Vector2(0, -5));
            isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + new Vector2(0, -2), 11);

            if (isGrounded)
            {
                hasJumped = false;
            }
            else if ((!isGrounded) && (!hasJumped)&&(!firstJump))
            { jump(); }
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX, gameObject.GetComponent<Rigidbody2D>().velocity.y);

    }

    public void walk(float direction)
    {
        moveX = direction * moveSpeed;
    }

    public void shoot()
    {

    }

    public void jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bossJumpPower);
        isGrounded = false;
        hasJumped = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            if (firstJump)
            {
                jump();
                firstJump = false;
            }
        }
    }
}
