using UnityEngine;

/// <summary>
/// Classe responsavel pelo controle do Rino.
/// </summary>
public class Rino : MonoBehaviour
{

    [Header("Rino movement")]
    [Tooltip("Rino move Speed")]
    public float Speed = 5.0f;

    [Tooltip("Rino move from right?")]
    public bool isRight = false;

    [Tooltip("Timer to change direction")]
    public float timer = 0;

    [Tooltip("Time to deslocation")]
    public float moveTime = 1.5f;

    /// <summary>
    /// Metodo default do unity, executa a cada x milisegundos com base na maquina que esta rodando.
    /// </summary>
    void Update()
    {
        // Execua a função de mover o rino.
        Move();
    }

    /// <summary>
    /// Metodo default do unity, executa quando a uma colisão entre GameObjects
    /// </summary>
    /// <param name="collision">Referencia ao GameObject que colidiu.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a colisão aconteceu com o Player.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Pega o "script" anexado ao Player
            Player player = collision.gameObject.GetComponent<Player>();
            // Envia uma "mensagem" para o script do player para executar a função KillFeedback
            player.SendMessage("KillPlayer", SendMessageOptions.DontRequireReceiver);
        }
    }

    /// <summary>
    /// Metodo de movimentação do rino.
    /// </summary>
    void Move()
    {
        // coloca o rino em movimento, como padrão para a esquerda.
        transform.Translate(Vector2.left * Speed * Time.deltaTime);

        // Somatorio do tempo de o rino andou em uma direção.
        timer += Time.deltaTime;

        // Verifica se o o rino ja andou o tempo maximo em uma direção.
        if (timer >= moveTime)
        {
            // Muda a "direção" domovimento do rino.
            isRight = !isRight;
            // Reseta o timer.
            timer = 0.0f;
            // Verifica se esta virado pra direita.
            if (isRight)
            {
                // Seta a rotação do rino pra 180° em Y (Vira ele pra direita).
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            else
            {
                // Seta a rotação do rino pra 0° em Y (Vira ele pra esquerda).
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
        }
    }

    /// <summary>
    /// Metod para destruir o GameObject Rino.
    /// </summary>
    void Die()
    {
        // Função da Unity para "destruir" o GameObject.
        Destroy(this.gameObject);
    }
}
