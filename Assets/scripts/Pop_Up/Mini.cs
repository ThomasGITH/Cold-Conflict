using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mini : MonoBehaviour {

    public GameObject textCloud, text;
    private Text message;
    private SpriteRenderer textBar;
    public int waveNumber;
    private float moveSpeed = 0.5f;
    public float transparency;
    public Color spriteColor;
    public bool triggerIntro;
    private string textMessage, currentMessage = "";

    public static string intro1 = "Defend your base!", intro2 = "You better defend the base!", intro3 = "We are under attack! Defend!", intro4 = "The humans are coming! Prepare yourself!";


    public static string instructions = "To pause the game, or to see the controls overview, press the buttons in the top-right corner of the screen.";
    public static string fact1 = "Every martian summer, roughly every two years, you get a higher chance of global dust storms. These can last for weeks, and the light from the sun drops by over 99%. During the dust storms, the artificial light is needed in the middle of the day to grow crops, and you won't be able to see anything. Solar power won't work.",
    fact2 = "Mars is far colder than Earth. At the Curiosity site, which is close to the equator, typical night-time temperatures are -70 ℃. Occasionally it drops below -100 ℃. It is often cold enough for the CO2 in the atmosphere to freeze out as dry ice. A human couldn't survive those temperatures without technology. ",
    fact3 = "This is the third fact. Perhaps my creator will write something interesting here one day. ",
    fact4 = "This is the fourth fact. Perhaps my creator will write something interesting here one day. ",
    fact5 = "... ",
    fact6 = "... ",
    fact7 = "... ",
    fact8 = "... ",
    fact9 = "... ",
    fact10 = "... ",
    fact11 = "... ";


    private string[] messageList = {instructions, fact1, fact2, fact3, fact4, fact5, fact6, fact7, fact8, fact9, fact10, fact11 };

    public string randomMessage = "WASSUUUUUP";

    public void pickIntro()
    {
        int randomNumber = Random.Range(0, 1000);
        if (triggerIntro)
        {
            if (randomNumber <= 245)
            {
                textMessage = intro1;
            }
            else if ((randomNumber > 245) && (randomNumber <= 490))
            {
                textMessage = intro2;
            }
            else if ((randomNumber > 490) && (randomNumber <= 735))
            {
                textMessage = intro3;
            }
            else if ((randomNumber > 735) && (randomNumber <= 980))
            {
                textMessage = intro4;
            }
            else
            {
                textMessage = randomMessage;
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if(!triggerIntro)
        {
            if (waveNumber == 0)
            {
                textMessage = messageList[waveNumber];
            }
            else
            {
                textMessage = "Fun fact: " + messageList[waveNumber];
            }
        }

        message = text.GetComponent<Text>();
        textBar = textCloud.GetComponent<SpriteRenderer>();

        if (spriteColor.a >= 0.9)
        {
            if ((Input.GetKeyDown(KeyCode.Q))||(Input.GetKeyDown(KeyCode.M)))
            {
                Time.timeScale = 1;
                StartCoroutine("FadeAway");
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

            spriteColor = GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.g , i);
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
            if(i <= 0.05f)
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
            currentMessage = textMessage.Substring(0,i);
            message.text = currentMessage;
            yield return new WaitForSecondsRealtime(0.025f);
        }
    }

}
