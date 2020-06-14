using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class Buttons : MonoBehaviour
{
    public GameObject pausedScreen;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("3654750", true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausedScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausedScreen.SetActive(!true);
    #if UNITY_ADS
        if (UnityAdControle.showAds)
        {
            UnityAdControle.ShowAd();
        }
    #endif
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
