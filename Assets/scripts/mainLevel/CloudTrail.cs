using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudTrail : MonoBehaviour {

    public GameObject player;
    public GameObject cloudTrail;
    private movement mov;
    private float moveX;

    private void Start()
    {
        mov = player.gameObject.GetComponent<movement>();
    }

    // Update is called once per frame
    void Update () {

        moveX = mov.moveX;

        if (moveX != 0.0f)
        {
            print("TEST");
            StartCoroutine("Cloud");
        }

        print(moveX);

	}

    IEnumerator Cloud()
    {
        if (mov.isGrounded)
        {
            var dust = Instantiate(cloudTrail,new Vector2(player.transform.position.x, player.transform.position.y),player.transform.rotation);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
