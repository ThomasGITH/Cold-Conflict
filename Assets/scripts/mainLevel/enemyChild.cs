using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChild : MonoBehaviour {

    public bool isGrounded;
    public Color groundColor;
    public GameObject fallImpact;
    private ParticleSystem impactParticles;

    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "ground") || (collision.gameObject.tag == "platform") || (collision.gameObject.tag == "base") || (collision.gameObject.tag == "powerUp"))
        {
            if(collision.gameObject.tag == "ground"){
                groundColor = new Color(1, 121f / 255f, 0);
            }
            else
            {
                groundColor = Color.gray;
            }
            isGrounded = true;
            print("GROUNDED");
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
            //if (!(collision.gameObject.tag == "ground") || !(collision.gameObject.tag == "platform") || !(collision.gameObject.tag == "base"))
            //{
            isGrounded = false;
             print("NOT GROUNDED");
        //}
    }
}
