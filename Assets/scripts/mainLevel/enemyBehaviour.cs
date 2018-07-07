using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour {

    private int enemyJumpPower = 450; //435
    private float moveX, enemySpeed = 6, myWidth;
    private bool isGrounded, hasJumped = true, facingLeft = false, detectsPlayer = false, isdead = false;
    public GameObject bulletPrefab, powerUp, shield, bloodSplash;
    public AudioClip shoot_sound, death_sound;
    public AudioSource track;
    GameObject player, home_base, shop;
    Animator animator;
    int shootTimer, playerHealth, health = 100, despawnTimer = 150, fallTime = 200, focusPoint;

    movement mov;

    private void Start()
    {
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        if(playerList.Length > 0){player = playerList[0];}
        GameObject[] baseList = GameObject.FindGameObjectsWithTag("base");
        if (baseList.Length > 0) { home_base = baseList[0]; }
        myWidth = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
        GameObject[] shopList = GameObject.FindGameObjectsWithTag("shop");
        if(shopList.Length > 0) { shop = shopList[0]; }
        focusPoint = Random.Range(0,10);

    }

    // Update is called once per frame
    void Update () {
       if (!isdead)
        {
            enemyMovement();
            enemyShoot();
        }
        else
        {
            death();
        }
	}

    void enemyMovement()
    {
        animator = gameObject.GetComponent<Animator>();
        
        int playerHealth = player.gameObject.GetComponent<movement>().health;

        Vector2 lineCastPosOld = new Vector2(transform.position.x - 0.715f, transform.position.y);
        //Vector2 lineCastPos = transform.position - transform.right * myWidth;
        Debug.DrawLine(lineCastPosOld, lineCastPosOld + new Vector2(0,-2));
        isGrounded = Physics2D.Linecast(lineCastPosOld, lineCastPosOld + new Vector2(0, -2),11);

        if (isGrounded)
        {
            hasJumped = false;
        }
        else if((!isGrounded)&&(!hasJumped))
        { jump(); }

        //print("GROUNDED: " + isGrounded + " HASJUMPED: " + hasJumped);
        if (((player != null)&&(player.gameObject.GetComponent<movement>().health > 0))&& ((home_base.GetComponent<baseBehaviour>().health > 0)))
        {
                if (detectsPlayer)
                {
                    moveX = 0.0f;
                    animator.SetBool("shoot", true);
                    animator.SetBool("walking", false);
                }
                else if(focusPoint > 5)
                {
                    moveX = player.gameObject.transform.position.x - gameObject.transform.position.x;
                    if(Mathf.Abs(moveX) > 1.0f) { if (facingLeft) { moveX = 1; } else { moveX = -1; } }
                    if ((moveX >= 0.01f)|| (moveX <= -0.01f))
                    {
                        animator.SetBool("walking", true); animator.SetBool("idle", false);
                    }
                    else { animator.SetBool("walking", false); animator.SetBool("idle", true); }

                }
                else if (focusPoint <= 5)
                {
                    if ((home_base.gameObject.transform.position.x < gameObject.transform.position.x) && (playerHealth > 0))
                    {
                        moveX = -1;
                        animator.SetBool("walking", true); animator.SetBool("idle", false);
                    }
                    else if ((home_base.gameObject.transform.position.x > gameObject.transform.position.x) && (playerHealth > 0))
                    {
                        moveX = 1;
                        animator.SetBool("walking", true); animator.SetBool("idle", false);
                    }
            }

        }
        else { moveX = 0.0f; animator.SetBool("idle", true); animator.SetBool("walking", false); animator.SetBool("shoot", false); }
        //print("DETECTS PLAYER: " + detectsPlayer);

        if ((moveX < 0) && (facingLeft)) { flipSprite(); }
        else if ((moveX > 0) && (!facingLeft)) { flipSprite(); }

        if (gameObject.transform.position.y <= -30)
        {
            track.clip = death_sound; track.Play();
            animator.SetTrigger("death");
            isdead = true;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * enemySpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void enemyShoot()
    {
        float rayStartPoint;
        if (!facingLeft) { rayStartPoint = transform.position.x - 2; }
        else { rayStartPoint = transform.position.x + 2; }
        RaycastHit2D hit;
        hit = Physics2D.Raycast(new Vector2(rayStartPoint,transform.position.y),new Vector2(moveX,0),10);
        Debug.DrawRay(new Vector2(rayStartPoint, transform.position.y), new Vector2(moveX, 0), Color.red);

        shootTimer -= 1;

        if (hit.collider != null)
        {
            mov = new movement();

            if ((hit.collider.tag == "Player")||(hit.collider.tag == "base"))
            {
                detectsPlayer = true;
                shootTimer = 100;
            }
        }

        if(shootTimer <= 0) { animator.SetBool("shoot", false); detectsPlayer = false; }
        //print("COLLIDER: " + hit.collider + " DETECTS PLAYER: " + detectsPlayer);
    }

    void shoot()
    {
        float startPoint; int direction;
        if (!facingLeft) { startPoint = -2.4f; direction = 0; } else { startPoint = 2.4f; direction = 1; }
        track.clip = shoot_sound; track.Play();
        var projectile = (GameObject)Instantiate(bulletPrefab, new Vector2(transform.position.x + startPoint, transform.position.y + 0.35f), transform.rotation);
        projectile.GetComponent<enemyBullet>().direction = direction;
    }

    void jump()
    {
        //JUMPING CODE
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * enemyJumpPower);
        isGrounded = false;
        hasJumped = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.tag == ("bullet"))
            {                
                if (health > 0)
                {
                var blood = Instantiate(bloodSplash,transform.position,transform.rotation);
                    Destroy(other.gameObject);
                }
                health -= 50;
                if (health <= 0)
                {
                    if (!isdead)
                    {
                        track.clip = death_sound; track.Play();
                        animator.SetTrigger("death");
                        isdead = true;
                    }
                }
            }
    }

    void flipSprite()
    {
        facingLeft = !facingLeft;
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y += 180;
        transform.eulerAngles = currentRotation;
    }


    void death()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        fallTime -= 1;
        if ((gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)&& (fallTime <= 0))
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
            despawnTimer -= 1;
            if (despawnTimer <= 0)
            {
            int randomNumber = Random.Range(0,100);
                if(randomNumber > 80)
                {
                    int otherRandomNumber = Random.Range(0, 100);
                    if(otherRandomNumber <= 35)
                    {
                        var shieldPickup = (GameObject)Instantiate(shield, new Vector2(transform.position.x, transform.position.y + 0.8f), transform.rotation);
                    }
                    else
                    {
                        var powerup = (GameObject)Instantiate(powerUp, new Vector2(transform.position.x, transform.position.y + 0.8f), transform.rotation);
                    }
                }
            shop.gameObject.GetComponent<SHOP>().PTS += 10;
            Destroy(gameObject);
            }
    }

    void flipColliderRotation()
    {
        //GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x * 2, GetComponent<BoxCollider2D>().size.y / 2);
    }
}
