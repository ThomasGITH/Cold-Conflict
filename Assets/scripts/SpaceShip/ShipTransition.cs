using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipTransition : MonoBehaviour {

    public GameObject progress, player;
    private float progress_percentage;
    Scene currentScene;

    private void Start()
    {
         currentScene = SceneManager.GetActiveScene();

    }

    void changeScene()
    {
         SceneManager.LoadScene(currentScene.name == "base" ? "Attack" : "base");
    }

    private void Update()
    {

        if (currentScene.name == "Attack")
        {
            progress_percentage = progress.GetComponent<Progress>().progress_percentage;
        }

        if((Mathf.RoundToInt(progress_percentage) >= 100)&&( player.gameObject.transform.position.x <= transform.position.x)&& (currentScene.name == "Attack"))
        {
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.position = new Vector2(21.7f, -1f);
            GlobalControll.Instance.number_of_wave += 1;
            transform.localScale *= 0.7f;
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("fly", true);
        }
    }
}
