using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackIntro : MonoBehaviour {

    public GameObject textCloud, text;
    private Text message;
    public Text progress_text;
    private SpriteRenderer textBar;
    private float moveSpeed = 0.5f;
    public float transparency;
    public Color spriteColor;
    public bool triggerIntro;
    private string textMessage, currentMessage = "";

    private string introMessage = "Destroy their equipment!";

    // Use this for initialization
    void Start () {
        int randomNumber = Random.Range(0,100);
        if(randomNumber <= 95)
        {
            textMessage = introMessage;
        }
        else
        {
            textMessage = "Time to kick some human ass";
        }
        StartCoroutine("TypeWriterFX");
    }

    // Update is called once per frame
    void Update () {

        if (spriteColor.a >= 0.9)
        {
            if ((Input.GetKeyDown(KeyCode.Q)) || (Input.GetKeyDown(KeyCode.M)))
            {
                Time.timeScale = 1;
                StartCoroutine("FadeAway");
                progress_text.enabled = true;
            }
        }

    }

    IEnumerator Appear()
    {
        for (float i = 0f; i < 1.0f; i += 0.04f)
        {
            message = text.GetComponent<Text>();
            textBar = textCloud.GetComponent<SpriteRenderer>();
            Time.timeScale = 0;

            spriteColor = GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.g, i);
            textBar.color = spriteColor;
            message.color = spriteColor;
            GetComponent<SpriteRenderer>().color = spriteColor;
            yield return null;
        }
    }

    IEnumerator FadeAway()
    {
        for (float i = 1; i >= 0.0f; i -= 0.04f)
        {
            spriteColor = GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.g, i);
            textBar.color = spriteColor;
            message.color = spriteColor;
            GetComponent<SpriteRenderer>().color = spriteColor;
            if (i <= 0.05f)
            {
                spriteColor.a = 1;
                gameObject.SetActive(false);
                triggerIntro = false;
            }
            yield return null;
        }
    }

    IEnumerator TypeWriterFX()
    {
        for (int i = 0; i <= textMessage.Length; i++)
        {
            currentMessage = textMessage.Substring(0, i);
            message.text = currentMessage;
            yield return new WaitForSecondsRealtime(0.025f);
        }
    }
}
