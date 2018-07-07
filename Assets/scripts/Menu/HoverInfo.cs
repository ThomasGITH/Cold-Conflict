using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverInfo : MonoBehaviour {

    public GameObject InfoItem;

    public void ShowInfo()
    {
        InfoItem.SetActive(true);
    }

    public void StopShowingInfo()
    {
        InfoItem.SetActive(false);
    }
}
