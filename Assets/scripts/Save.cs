using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

    public GameObject enemy_spawn;

    public void saveProgress()
    {
        GlobalControll.Instance.number_of_wave = enemy_spawn.GetComponent<enemySpawn>().number_of_wave;
    }

}
