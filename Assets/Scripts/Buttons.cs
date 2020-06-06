using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Log()
    {
        Debug.Log("Fechar pressionado!");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Debug.Log("Resumed");
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
