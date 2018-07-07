using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SHOP : MonoBehaviour {

    public GameObject shopContent, home_base, player, health, armor, baseHealth;
    public Text Number_of_Points, healthCost, armorCost, baseHealthCost;
    public Sprite enabledIcon, disabledIcon;
    public bool shopIsActive;
    public Image img;
    public RectTransform dimensions;
    public int PTS;
    public AudioSource track;
    public AudioClip purchase, declined;
    Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        PTS = GlobalControll.Instance.points;

        if(currentScene.name == "Attack")
        {
            healthCost = health.GetComponent<Text>();
            armorCost = armor.GetComponent<Text>();
            baseHealthCost = baseHealth.GetComponent<Text>();

            healthCost.text = "SOLD";
            armorCost.text = "SOLD";
            baseHealthCost.text = "SOLD";
        }
    }

    // Update is called once per frame
    void Update () {
        Number_of_Points.text = "PTS: " + PTS;
        GlobalControll.Instance.points = PTS;

    }

    public void changeState()
    {
        img.sprite = shopIsActive ? disabledIcon : enabledIcon;
        shopIsActive = !shopIsActive;
        if (shopIsActive)
        {
            dimensions.sizeDelta = new Vector2(2.563f, 2.782f);
            shopContent.SetActive(true);
        }
        else
        {
            dimensions.sizeDelta = new Vector2(0.277f, 0.3f);
            shopContent.SetActive(false);
        }

    }

    public void PurchaseHealth()
    {
        if ((PTS >= 140)&&(currentScene.name != "Attack"))
        {
            PTS -= 140;
            track.clip = purchase;
            track.Play();
            player.gameObject.GetComponent<movement>().health = 100;
        }
        else
        {
            track.clip = declined;
            track.Play();
        }
    }

    public void PurchaseArmor()
    {
        if ((PTS >= 50)&& (currentScene.name != "Attack"))
        {
            PTS -= 50;
            track.clip = purchase;
            track.Play();
            player.gameObject.GetComponent<movement>().shieldHealth = 4;
        }
        else
        {
            track.clip = declined;
            track.Play();
        }
    }

    public void PurchaseBaseHealth()
    {
        if((PTS >= 300)&& (currentScene.name != "Attack"))
        {
            PTS -= 300;
            track.clip = purchase;
            track.Play();
            home_base.gameObject.GetComponent<baseBehaviour>().health = home_base.gameObject.GetComponent<baseBehaviour>().totalHealth;
        }
        else
        {
            track.clip = declined;
            track.Play();
        }
    }


}
