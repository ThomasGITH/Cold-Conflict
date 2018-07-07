using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropship : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer sr;
    private float moveX, moveY;
    public float speed = 10;
    public GameObject astronaut,boss, cybernaut;
    public int waveNumber;
    private  int stage = 0, i = 0, j = 0, dropTimer;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
        DestroywhenOutOfRange();
        if (sr.flipX == false)
        {
            //Y VELOCITY
            if (transform.position.y <= 9.9f)
            {
                moveY = 0.0f;
            }
            else if (transform.position.y > 9.9f)
            {
                moveY = -1 * speed;
            }

            //X VELOCITY
            if (transform.position.x <= 27.9f)
            {
                moveX = 0.0f;
            }
            else if (transform.position.x > 27.9f)
            {
                moveX = -1 * speed;
            }
        }

        rb.velocity = new Vector2(moveX, moveY);

        if(stage == 1) {
            dropTimer -= 1;
            if(dropTimer <= 0)
            {
                dropOff();
                dropTimer = 50;
            }
        }
        else if (stage == 2)
        {
            moveX = 1 * speed; moveY = 1 * speed;
            if (sr.flipX == false)
            {
                sr.flipX = true;
            }
        }

        if(((moveX == 0.0f)&&(moveY == 0.0f))&&(stage != 2))
        {
            stage = 1;
        }

	}

    void dropOff()
    {
        //INSTANTIEER DE ENEMY'S

        Wave wave = new Wave(waveNumber);
        
        if (i < wave.getAstronautCount())
        {
            i++;
            int randomNumber = Random.Range(0,100);
            if(randomNumber >=50)
            {
                var enemy = (GameObject)Instantiate(astronaut, new Vector2(Random.Range(transform.position.x - 2, transform.position.x + 5), Random.Range(transform.position.y - 1, transform.position.y + 1)), transform.rotation);
            }
            else {
                var enemy = (GameObject)Instantiate(cybernaut, new Vector2(Random.Range(transform.position.x - 2, transform.position.x + 5), Random.Range(transform.position.y - 1, transform.position.y + 1)), transform.rotation);
            }
        }
        else { stage = 2; }
        
        if (j < wave.getBossCount())
        {
            j++;
            var largeEnemy = (GameObject)Instantiate(boss, new Vector2(transform.position.x, transform.position.y), transform.rotation);
        }
        
    }

    private void DestroywhenOutOfRange()
    {
        if((transform.position.x  < -70)|| (transform.position.x > 70) || (transform.position.y > 50)|| (transform.position.y < -50))
        {
            Destroy(gameObject);
        }
    }

}
