using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnemy : MonoBehaviour {

    public GameObject bullet;
    private GameObject player, shop;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public AudioSource audSource;
    public AudioClip laser, death;

    private float moveX = 0.0f;
    public float speed = 1;
    public int health = 100;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shop = GameObject.FindGameObjectWithTag("shop");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {

        if(health > 0)
        {
            //FLIP SPRITE TO FACE PLAYER
            flipSprite(player.gameObject.transform.position.x < transform.position.x ? true : false);

            if (inRange(10))
            {
                shoot();
            }
            else if (inRange(30))
            {
                walk();
            }
            else
            {
                idle();
            }
        }

        
        //APPLY NEW MOVEMENT ONTO RIGIDBODY
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveX, GetComponent<Rigidbody2D>().velocity.y);
        animator.SetFloat("hSpeed", moveX);
        moveX = 0.0f; // RESET THE APPLY-VALUE
	}

    bool inRange(float distance)
    {
        bool detection = false;
        float rayStartPoint;

        if (isFlipped())
        {
            rayStartPoint = transform.position.x - 1;
        }
        else { rayStartPoint = transform.position.x + 1; }

        RaycastHit2D hit;
        hit = Physics2D.Raycast(new Vector2(rayStartPoint, transform.position.y), new Vector2(isFlipped() ? -1 : 1, 0), distance);
        Debug.DrawRay(new Vector2(rayStartPoint, transform.position.y), new Vector2(isFlipped() ? -1 : 1, 0), Color.red);

        if(hit.collider != null)
        {
            detection = hit.collider.tag == "Player" ? true : false;
        }

        return detection;
    }

    void walk()
    {
        animator.SetBool("shooting", false);

        moveX = player.gameObject.transform.position.x - gameObject.transform.position.x;
        moveX = Mathf.Clamp(moveX, -1 * speed, 1 * speed);
    }

    void shoot()
    {
        animator.SetBool("shooting", true);
    }

    void createProjectile()
    {
        float startPoint = isFlipped() ? -1.75f : 1.75f;

        audSource.clip = laser;
        audSource.Play();
        var projectile = (GameObject)Instantiate(bullet, new Vector2(transform.position.x + startPoint, transform.position.y + 0.20f), transform.rotation);
        projectile.GetComponent<enemyBullet>().direction = isFlipped() ? 0 : 1;
    }

    void idle()
    {
        animator.SetBool("shooting", false);
    }

    void flipSprite(bool flipped)
    {
        spriteRenderer.flipX = flipped;
    }

    bool isFlipped()
    {
        return spriteRenderer.flipX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "bullet")&&(health > 0))
        {
            health -= 34;
            Destroy(collision.gameObject);
        }

        if(health <= 0)
        {
            if(audSource.clip != death)
            {
                audSource.clip = death;
                audSource.pitch = 2;
                audSource.Play();
            }
            animator.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine("deSpawn");
        }
    }

    IEnumerator deSpawn()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            if(f <= 0.1f)
            {
                shop.gameObject.GetComponent<SHOP>().PTS += 15;
                Destroy(gameObject);
            }
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }


}

