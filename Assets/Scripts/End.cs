using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe responsavel pelo fim de jogo "Congratulations"
/// </summary>
public class End : MonoBehaviour
{

    /// <summary>
    /// Metodo default do unity, executa quando a uma colisão entre GameObjects
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a clisão foi com um GameObject com a tag "PLAYER"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Carrega a cena de Congratulation / Fim de Joga.
            SceneManager.LoadScene("Congratulation");
        }

    }
}
