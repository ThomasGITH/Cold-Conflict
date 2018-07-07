using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControll : MonoBehaviour
{
    public static GlobalControll Instance;

    public int number_of_wave;
    public int points;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
