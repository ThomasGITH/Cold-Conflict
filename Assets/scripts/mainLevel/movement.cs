using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour {

    public int playerSpeed = 10, playerJumpPower = 1250, health = 100, deathTimer = 120, fallTime = 200, shieldHealth = 4;
    private int flipTimer, jumpsLeft = 2, direction, respawnTimer = 150, wavenumber = 1; //MAKES SURE ANIMATIONS DON'T FLIP DON'T FLIP TOO OFTEN
    public GameObject bulletPrefab, camera, drop_ship, home_base, backGround, blood, shieldSplash, dustTrail, shop;
    public TextEditor text;
    public AudioClip shootSound, hitSound, dyingSound, fallSound, boom;
    public AudioSource track, backGroundTrack;
    private bool facingLeft = true, isDead = false;
    public bool isGrounded = false;
    public float moveX, backGroundVolume;
    Animator animator;
    public AnimationClip runShoot, run, crouch;
    public int score = 0;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public Collider2D main;

    // Update is called once per frame
    void Update () {
        
        if (!isDead)
        {
            playerMove();
            playerShoot();
            aliveCheck();
            baseCheck();
        }else{Death();}

        if(track.clip == shootSound) { track.pitch = 1.75f; } else { track.pitch = 1; }
    }
    //GENERAL MOVEMENT

    private void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void playerMove()
    {
        isGrounded = GetComponentInChildren<enemyChild>().isGrounded;

        if (isGrounded) { jumpsLeft = 1; }
        //CONTROLS
        crouch.wrapMode = WrapMode.Once;

        if ((Input.GetKey(KeyCode.S))&&(isGrounded))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("crouch")){
                animator.SetBool("isCrouching", true);
                animator.SetTrigger("crouch");
            }
           main.enabled = false;
            moveX = 0.0f;
        }
        else if((!animator.GetCurrentAnimatorStateInfo(0).IsName("powerUp")))
        {
            moveX = Input.GetAxis("Horizontal");
            animator.SetBool("isCrouching", false);
            main.enabled = true;
            animator.speed = 1;
        }
        else { moveX = 0.0f; }

        float speed = moveX;
        if(moveX < 0) { speed *= -1; }

        animator.SetFloat("vSpeed", gameObject.GetComponent<Rigidbody2D>().velocity.y);
        animator.SetFloat("hSpeed", speed);
        animator.SetBool("grounded", isGrounded);

        flipTimer -= 1;
        //PLAYER DIRECTION
        if(moveX < 0.0f && facingLeft == false)
        {
            if(flipTimer <= 0)
            {
                FlipPlayer();
            }
        }
        else if(moveX > 0.0f && facingLeft == true)
        {
            if (flipTimer <= 0)
            {
                FlipPlayer();
            }
        }

        //JUMP && GROUNDED-CHECK
        if ((Input.GetButtonDown("Jump"))|| (Input.GetKeyDown(KeyCode.W)))
        {
            if ((isGrounded)||(jumpsLeft > 0))
            {
                jump();
            }
        }

        //RUNNING TRAIL PARTICLE EFFECT
        var emission = dustTrail.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = isGrounded ? Mathf.Abs(moveX) * 10 : 0;

        var particleMain = dustTrail.GetComponent<ParticleSystem>().main;
        particleMain.startColor = GetComponentInChildren<enemyChild>().groundColor;

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        animator = gameObject.GetComponent<Animator>();
    }

    //SHOOTING MECHANICS
    void playerShoot()
    {
            //MOUSE TRACKING, CONVERTS SCREEN TO WORLDPOINT
            Vector2 mousePos = Input.mousePosition;
            mousePos = camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

            if (playerSpeed < 10) { playerSpeed += 1; }

            //FIRE BULLETS
            if ((Input.GetButtonDown("Fire1"))&&(animator.GetBool("isCrouching") == false) && (!shop.gameObject.GetComponent<SHOP>().shopIsActive))
            {
                //playerSpeed -= 4;
                if ((mousePos.x > transform.position.x) && (mousePos.y < transform.position.y + 3))
                {
                    if (facingLeft) { FlipPlayer(); flipTimer = 35; }
                    direction = 1;
                }
                else if ((mousePos.x < transform.position.x) && (mousePos.y < transform.position.y + 3))
                {
                    if (!facingLeft) { FlipPlayer(); flipTimer = 35; }
                    direction = 0;
                }
                else if (mousePos.y > transform.position.y + 3)
                {
                    if (mousePos.x > transform.position.x)
                    {
                        if (facingLeft) { FlipPlayer(); flipTimer = 35; }
                    }
                    else { if (!facingLeft) { FlipPlayer(); flipTimer = 35; } }
                direction = 2;
                }
            animator.SetBool("isShooting", true);
            if(direction <= 1)
            {
                animator.SetTrigger("shoot");
            }
            else
            {
                animator.SetTrigger("shootUp");
            }
            animator.SetTrigger("continuee");
        }
        else
        {
            animator.SetBool("isShooting", false);
            animator.SetTrigger("continue");
        }
    }

    void shoot()
    {
        float startPoint;
        if(direction == 0)
        {
            if ((animator.GetFloat("hSpeed") > -0.01) && (animator.GetFloat("hSpeed") < 0.01))
            {
                startPoint = -2;
            }
            else { startPoint = -2.5f; }
        }
        else if(direction == 1){
            if ((animator.GetFloat("hSpeed") > -0.01) && (animator.GetFloat("hSpeed") < 0.01))
            {
                startPoint = 2;
            }
            else
            { startPoint = 2.5f; }
        }
        else
        {
            startPoint = 0;
        }

       float runORwalk;

        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("runShoot"))|| (animator.GetCurrentAnimatorStateInfo(0).IsName("walk")))
        {
            if(gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
            {
               //runShoot.wrapMode
            }
            else { animator.speed = 1; }
        }
        if ((animator.GetFloat("hSpeed") > -0.01) && (animator.GetFloat("hSpeed") < 0.01))
        {
            runORwalk = 1;
        }
        else { runORwalk = 0.6f; }

        if(direction == 2)
        {
            runORwalk = 3;
        }

        track.clip = shootSound;
        track.Play();
        var projectile = (GameObject)Instantiate(bulletPrefab, new Vector2(transform.position.x + startPoint, transform.position.y + runORwalk), transform.rotation);
        projectile.GetComponent<bullet>().direction = direction;
    }

    int stage = 0, gameOverTimer = 150;
    void aliveCheck()
    {
        if (gameObject.transform.position.y < -19)
        {
            if (stage < 1)
            {
                backGround.GetComponent<AudioSource>().volume = 0.0f;
                track.clip = fallSound; track.pitch = 0.5f; track.Play();
                stage = 1;
            }
            if ((!track.isPlaying)&&(stage < 2))
            {
                track.clip = boom; track.pitch = 0.5f; track.Play();
                stage = 2; gameOverTimer -= 1;
            }
            if (gameOverTimer < 150)
            {
                gameOverTimer -= 1;
                if(gameOverTimer <= 0)
                {
                    isDead = true;
                    Death();
                }
            }
            if (!track.isPlaying)
            {

            }
        }
    }

    void baseCheck()
    {
        if(home_base != null)
        {
            int baseHealth = home_base.GetComponent<baseBehaviour>().health;
            if (baseHealth <= 0)
            {
                animator.SetTrigger("cri");
                if (!facingLeft)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                isDead = true;
            }
        }
    }

    void jump()
    {
        //JUMPING CODE
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        jumpsLeft -= 1;
    }

    void FlipPlayer()
    {
        facingLeft = !facingLeft;
        GetComponent<SpriteRenderer>().flipX = facingLeft ? false : true;
        //Vector2 localScale = gameObject.transform.localScale;
        //localScale.x *= -1;
        //transform.localScale = localScale;
    }

    void Death()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        fallTime -= 1;
        if ((gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)&& (fallTime <= 0))
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        respawnTimer -= 1;
            if (respawnTimer <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.tag == ("enemyBullet"))||(collision.tag == ("bossBullet")))
        {
                Destroy(collision.gameObject);
            if (shieldHealth > 0) { shieldHealth -= 1; var shieldSpatter = Instantiate(shieldSplash, transform.position, transform.rotation); }
                else
                {
                var bloodSpatter = Instantiate(blood, transform.position,transform.rotation);
                    health -= 10;
                }
                if (health <= 0)
                {
                    if (animator.GetBool("isDead") == false)
                    {
                        animator.SetTrigger("ded");
                        animator.SetBool("isDead", true);
                        track.clip = hitSound; track.Play();
                        isDead = true;
                    }
                }
                else { track.clip = hitSound; track.Play();}
        }
            if(collision.gameObject.tag == ("powerUp"))
            {
                animator.SetTrigger("healUp");
                Destroy(collision.gameObject);
                health += 20;
            }

        if (collision.gameObject.tag == ("shield"))
            {
                shieldHealth = 4;
                Destroy(collision.gameObject);
            }

        if (health < 0) { health = 0; }
        else if(health > 100) { health = 100; }
    }

    void pauseCrouch()
    {
        animator.speed = 0.0f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("GFL"))
        {
            if (shieldHealth > 0) { shieldHealth -= 1; }
            else
            {
                health -= 1;
            }
            if (health <= 0)
            {
                if (animator.GetBool("isDead") == false)
                {
                    animator.SetTrigger("ded");
                    animator.SetBool("isDead", true);
                    track.clip = hitSound; track.Play();
                    isDead = true;
                }
            }
        }

    }

}
