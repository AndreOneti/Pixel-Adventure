using UnityEngine;

/// <summary>
/// Classe responsavel pelo controle do Disparo.
/// </summary>
public class ShootController : MonoBehaviour
{
    [Header("Shoot Variables")]
    [Tooltip("Direção do tiro")]
    public bool IsLeft = true;

    [Tooltip("Velocidade do tiro")]
    public float Speed;

    [Tooltip("Distancia maxima do tiro")]
    public float MaxDistance;

    [Tooltip("Referencia ao sistema de particulas")]
    public GameObject BuletDestroy;

    /// <summary>
    /// Posição inicial do tiro.
    /// </summary>
    Vector3 startingPosition;

    /// <summary>
    /// Metodo default do unity, executa antes das demais funções.
    /// </summary>
    void Start()
    {
        // Pega a posição inicial do tiro.
        startingPosition = transform.position;
    }

    /// <summary>
    /// Metodo default do unity, executa a cada x milisegundos com base na maquina que esta rodando.
    /// </summary>
    void Update()
    {
        // Chama a função de destruição do tiro.
        ShootDestroy();
    }

    /// <summary>
    /// Função de movimentação do tiro.
    /// </summary>
    public void Move()
    {
        // Verifica se esta para a esquerda.
        if (IsLeft)
        {
            // Seta o movimento do tiro para a esquerda.
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
        else
        {
            // Seta o movimento do tiro para a Direita.
            transform.Translate(Vector2.left * -Speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Função resonsavel pela verificação do tiro.
    /// Verifica se move ou é destruido.
    /// </summary>
    public void ShootDestroy()
    {
        // Pega a distanca atual do tiro referente ao seu ponto inicial de spawn.
        float currentDistance = Vector3.Distance(startingPosition, transform.position);
        // Veriica se esta na distancia maxima dele.
        if (currentDistance > MaxDistance)
        {
            // Se tiver chama a função de destruição do tiro.
            Explode();
        }
        else
        {
            // Senão chama a função de movimentação do tiro.
            Move();
        }
    }

    /// <summary>
    /// Função resopnsavel por destruir o tiro e instanciar as particulas.
    /// </summary>
    public void Explode()
    {
        // Verifica se tem referencia as particulas
        if (BuletDestroy != null)
        {
            // Instancia as particulas no mesmo local do tiro.
            var partciles = Instantiate(BuletDestroy, transform.position, Quaternion.identity);
            // Função da Unity para "destruir" o GameObject.
            // Passando um tempo de 1.5 segundos para destruir a paricula.
            Destroy(partciles, 1.5f);
        }
        // Função da Unity para "destruir" o GameObject.
        // Passando um tempo de 1.5 segundos para destruir o tiro.
        Destroy(this.gameObject, 0.08f);
    }

    /// <summary>
    /// Metodo default do unity, executa quando a uma colisão entre GameObjects
    /// </summary>
    /// <param name="collision">Referencia ao GameObject que colidiu.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a colisão aconteceu com o Player.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Chama a função de destruição do tiro.
            Explode();
            // Executa a função KillPlayer com base na colisão.
            collision.gameObject.GetComponent<Player>().KillPlayer();
        }

    }
}
