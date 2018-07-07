using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{

    public GameObject player, dialogue_guy;
    public float progress_amount = 0;
    public float progress_percentage;
    public Text progress_counter;
    private bool hasPlayed;

    public static string intro1 = "Deal as much damage as possible to the enemy base!";
    // 128 is 100%!!

    // Update is called once per frame
    void Update()
    {

        if((player.gameObject.transform.position.x >= 35.71f)&&(!hasPlayed))
        {
            dialogue_guy.SetActive(true);
            dialogue_guy.GetComponent<AttackIntro>().StartCoroutine("Appear");
            hasPlayed = true;
        }

        //progress_percentage = 161 / 100;
        //progress_percentage *= progress_amount;

        progress_counter.text = "Progress: " + Mathf.RoundToInt(progress_percentage) + "%";

    }

}
