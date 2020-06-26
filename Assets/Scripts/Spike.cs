using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe responsavel pelo controle do Spike
/// </summary>
public class Spike : MonoBehaviour
{
    /// <summary>
    /// Metodo default do unity, executa quando a uma colisão entre GameObjects
    /// </summary>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a clisão foi com um GameObject com a tag "PLAYER"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Carrega a cena de GameOver
            SceneManager.LoadScene("GameOver");
        }
    }
}
