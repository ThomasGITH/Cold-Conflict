using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateEasterEgg : MonoBehaviour {

    public GameObject FidgetSpinner;
    public Button m_Button;

    public float SpinSpeed = 1;
    private bool SpinOrNot = false;

    void Start()
    {
        SpinOrNot = false;
        m_Button.onClick.AddListener(stopmusic);
    }

    void Update()
    {
        if (SpinOrNot)
        {
            transform.Rotate(0, 0, SpinSpeed * Time.deltaTime);
        }

    }

    public void StartSpinning()
    {
        SpinOrNot = true;
        if (SpinOrNot)
        {
            SpinSpeed = SpinSpeed + 10;
        }
    }

    void stopmusic()
    {
        GetComponent<AudioSource>().Stop();
    }

}
