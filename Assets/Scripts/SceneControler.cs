using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler : MonoBehaviour
{
    /// <summary>
    /// Metodo default do unity, executa antes das demais funções.
    /// </summary>
    void Start()
    {
        // Salva o nome da cena atual.
        PlayerPrefs.SetString("currentScene", SceneManager.GetActiveScene().name);
    }
}
