using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class enemySpawn : MonoBehaviour {

    public GameObject dropShip, warning, textObject, cameraObj, popup, player, playerShip, background;
    GameObject enemyDropShip, watchOut, camera;
    GameObject waveCounter;

    public int number_of_wave = 0;
    private int timer = 300, enemiesLeft;
    private bool takenOff = false, hasExecuted = false, disablePopup, triggerIntro = false;

	// Use this for initialization
	void Start () {
        // waveCounter = textObject.GetComponent<Text>();
        //waveCounter.enabled = false;
        number_of_wave = GlobalControll.Instance.number_of_wave;
        textObject.SetActive(false);
	}


    void FixedUpdate() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemiesLeft = enemies.Length;
        
        if (triggerIntro)
        {
            if ((enemiesLeft <= 0) && (!takenOff))
            {
                if ((number_of_wave != 4) && (number_of_wave != 8) && (number_of_wave != 12) && (number_of_wave != 16))
                {
                timer -= 1;
                if (timer <= 0)
                {
                    number_of_wave++;
                    enemyDropShip = (GameObject)Instantiate(dropShip, new Vector2(transform.position.x, transform.position.y), transform.rotation);
                    enemyDropShip.GetComponent<dropship>().waveNumber = number_of_wave;
                    takenOff = true;
                    timer = 300;
                    hasExecuted = false;
                    GlobalControll.Instance.number_of_wave = number_of_wave;
                }
                else
                {
                    if (number_of_wave != 0)
                    {
                        if ((!hasExecuted) && (!disablePopup))
                        {
                            popup.SetActive(true);
                            popup.GetComponent<Mini>().spriteColor.a = 1;
                            popup.GetComponent<Mini>().StartCoroutine("Appear");
                            popup.GetComponent<Mini>().StartCoroutine("TypeWriterFX");
                            popup.GetComponent<Mini>().waveNumber = number_of_wave - 1;
                            hasExecuted = true;
                        }
                    }
                }
                }
                else {
                    print("ATTACK THEE ENEMY!"); playerShip.SetActive(true);
                    if (player.transform.position.x >= playerShip.transform.position.x - 2) {
                        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                        background.GetComponent<randomSong>().enabled = false;
                        background.GetComponent<AudioSource>().enabled = false;
                        playerShip.transform.position = new Vector2(41.09f, -1.33f);
                        playerShip.transform.localScale *= 0.7f;
                        playerShip.GetComponent<Animator>().SetBool("fly", true);
                        player.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }

            if(number_of_wave >= 8)
            {
                disablePopup = true; //TIJDELIJK!! VERWIJDER LATER
            }

            if (enemyDropShip == null) { takenOff = false; }

            if (timer <= 150)
            {
                warning.SetActive(true);
            }
            else { warning.SetActive(false); }

            int wavePortrayal = number_of_wave + 1;
            if (timer <= 150)
            {
                //waveCounter.enabled = true;
                textObject.SetActive(true);
                //waveCounter.text = "Wave " + wavePortrayal;
               // Animation waveAnimation = waveCounter.GetComponent<Animation>();
                //waveAnimation.Play();
            }
            else
            {
                textObject.SetActive(false);
                //waveCounter.enabled = false;
            }
        }

    }

    private void Update()
    {

        if((player.gameObject.transform.position.x >= -27.48)&&(triggerIntro == false))
        {
            popup.SetActive(true);
            popup.GetComponent<Mini>().spriteColor.a = 1;
            popup.GetComponent<Mini>().triggerIntro = true;
            popup.GetComponent<Mini>().pickIntro();
            popup.GetComponent<Mini>().StartCoroutine("Appear");
            popup.GetComponent<Mini>().StartCoroutine("TypeWriterFX");
            triggerIntro = true;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            disablePopup = true;
        }
    }

}