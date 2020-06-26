using UnityEngine;

/// <summary>
/// Classe responsavel pelo controle de dano deferido ao inimigo.
/// </summary>
public class HitEnimies : MonoBehaviour
{
    /// <summary>
    /// Enimy animator reference.
    /// </summary>
    [Header("Enimy Variables")]
    [Tooltip("Enimy animator reference")]
    [SerializeField]
    private Animator anim;

    /// <summary>
    /// Metodo default do unity, executa antes das demais funções.
    /// </summary>
    void Start()
    {
        // Pega o Animator do Inimigo.
        anim = transform.parent.gameObject.GetComponent<Animator>();
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
            player.SendMessage("KillFeedback", SendMessageOptions.DontRequireReceiver);
            // Roda a animação de dano do inimigo.
            anim.Play(anim.name + "_hit");
        }
    }
}