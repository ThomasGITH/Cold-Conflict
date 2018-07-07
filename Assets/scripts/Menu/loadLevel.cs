

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadLevel : MonoBehaviour
{
    public GameObject loadingscreen, quitbutton;
    public Text m_Text;
    public Button m_Button;
    public Slider slider;
    public AudioSource track;
    public AudioClip clip;

    void Start()
    {
        track = GetComponent<AudioSource>();
        m_Button.onClick.AddListener(LoadButton);
    }

    public void LoadButton()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        loadingscreen.SetActive(true);
        track.clip = clip;
        track.Play();
        if(quitbutton != null)
        {
            quitbutton.SetActive(false);
        }
        yield return null;

        int sceneIndex = 1;
        Scene currentScene = SceneManager.GetActiveScene();

        if ((currentScene.name == "Menu")||(currentScene.name == "_StartScherm"))
        {
            sceneIndex = 1;
        }
        else if (currentScene.name == "GameOver")
        {
            sceneIndex = 0;
            print("DETECTEDDDDDDDDDD");
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);

        while (!asyncOperation.isDone)
        {
            
            float progress = (asyncOperation.progress * 100);
            m_Text.text = (Mathf.RoundToInt(progress)) + "%";
            slider.value = asyncOperation.progress;

            if (asyncOperation.progress >= 0.9f)
            {
                slider.value = 1f;
                //m_Text.text = "100%";

                if(sceneIndex != 0)
                {
                    m_Text.text = "Press SPACE  to continue";
                    if (Input.GetKeyDown(KeyCode.Space))
                    asyncOperation.allowSceneActivation = true;
                }
                else { m_Text.text = "100%";

                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}