using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public GameObject LevelSelectPrefab;
    public string LevelName;

    public void OpenScene()
    {
        //LevelSelectPrefab.SetActive(false);
        SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
        //SceneManager.as
    }
}
