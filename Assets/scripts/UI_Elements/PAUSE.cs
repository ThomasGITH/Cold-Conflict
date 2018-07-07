using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PAUSE : MonoBehaviour {

    public GameObject dialogueGuy, controlsScreen;
    public Sprite on, off;
    public bool Paused;
    public Image img;

    public void changeState()
    {
        if ((!dialogueGuy.activeSelf)&&(!controlsScreen.activeSelf))
        {
            img.sprite = Paused ? off : on;
            Paused = !Paused;
            Time.timeScale = Paused ? 0 : 1;
        }
    }
}
