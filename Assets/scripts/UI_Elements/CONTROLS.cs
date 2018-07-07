using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CONTROLS : MonoBehaviour {

    public GameObject controlScreen, dialogueGuy, pause, shop;
    public Sprite enabledIcon, disabledIcon;
    private bool controlScreenIsActive;
    public Image img;

    public void changeState()
    {
        if ((!dialogueGuy.activeSelf)&&(!pause.gameObject.GetComponent<PAUSE>().Paused)&&(!shop.gameObject.GetComponent<SHOP>().shopIsActive))
        {
            img.sprite = controlScreenIsActive ? disabledIcon : enabledIcon;
            controlScreenIsActive = !controlScreenIsActive;
            controlScreen.SetActive(controlScreenIsActive);
            Time.timeScale = controlScreenIsActive ? 0 : 1;
        }
    }

}
