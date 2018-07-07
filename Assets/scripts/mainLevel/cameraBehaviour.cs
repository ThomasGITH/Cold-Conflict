using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehaviour : MonoBehaviour {

    public GameObject player;
    public GameObject background;
    public float cameraSpeed = 2;
    private float moveX, moveY;
    public double cameraMaxX = 27, cameraMinX = -28, cameraY = 9.2;

    // Update is called once per frame
    void Update () {
        cameraMovement();
    }

    void cameraMovement()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            //transform.Rotate(transform.rotation.x, transform.rotation.y, 45);
            GetComponent<Rigidbody2D>().angularVelocity = 30;
        }else if ((Input.GetKeyDown(KeyCode.P)))
        {
            Physics2D.gravity = new Vector2(Physics2D.gravity.x, Physics2D.gravity.y * -1);

        }

        if (player != null)
        {
            //HORIZONTAL MOVEMENT (Y)
            if ((player.gameObject.transform.position.x > cameraMaxX) || (player.gameObject.transform.position.x < cameraMinX))
            {
                if ((gameObject.transform.position.x > cameraMaxX) || (gameObject.transform.position.x < cameraMinX))
                {
                    moveX = 0.0f;
                }
            }
            else
            {
                float distanceX = player.gameObject.transform.position.x - gameObject.transform.position.x;
                moveX = distanceX * cameraSpeed;
            }

            //VERTICAL MOVEMENT (Y)
            if ((player.gameObject.transform.position.y > cameraY) || (player.gameObject.transform.position.y < -cameraY))
            {
                if ((gameObject.transform.position.y > cameraY) || (gameObject.transform.position.y < -cameraY))
                {
                    moveY = 0.0f;
                }
            }
            else
            {
                float distanceY = player.gameObject.transform.position.y - gameObject.transform.position.y;
                moveY = distanceY * cameraSpeed;
            }

        }
        else
        {
            moveX = 0.0f; moveY = 0.0f;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX, moveY);
    }
}
