using UnityEngine;

/// <summary>
/// Classe responsavel pelo controle da Plant.
/// </summary>
public class Plant : MonoBehaviour
{

    [Header("Plant variables")]
    [Tooltip("Referencia ao transform da Plant")]
    public Transform player;

    [Tooltip("Distancia maxima de verificação da planta com o Player")]
    public float agroRange;

    [Tooltip("Referencia ao ponto de spawn do disparos")]
    public Transform CastPoint;

    [Tooltip("Direção do tiro, normalmente pra esquerda")]
    public bool ShootToLeft = true;

    [Tooltip("Tempo de espera entre os disparos")]
    public float timeToShoot;

    [Tooltip("Referencia ao GameObject do tiro")]
    public GameObject shootPrefab;

    /// <summary>
    /// Tempo atual pra verificar o tempo entre os tiros.
    /// </summary>
    private float currentTime;

    /// <summary>
    /// Metodo default do unity, executa a cada x milisegundos com base na maquina que esta rodando.
    /// </summary>
    void Update()
    {
        // Somatorio do tempo para o disparo.
        currentTime += Time.deltaTime;
        // Chamada da função de execução/verificação do disparo.
        shoot();
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
    /// Metod para destruir o GameObject Plant.
    /// </summary>
    void Die()
    {
        // Função da Unity para "destruir" o GameObject.
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Função de execução/verificação do disparo.
    /// </summary>
    void shoot()
    {
        // Verifica se o player esta no range da planta.
        if (CanSeePlayer(agroRange))
        {
            // Verifica se esta na hora de disparar novamente.
            if (currentTime >= timeToShoot)
            {
                // Chama a função de disparo.
                spawaShoot();
                // Seta o tempo atual para zero, para recomeção o somatorio.
                currentTime = 0;
            }
        }
        else
        {
            // Verifica se esta na hora de disparar novamente.
            if (currentTime >= timeToShoot)
            {
                // Seta o tempo atual para zero, para recomeção o somatorio.
                currentTime = 0;
            }
        }
    }

    /// <summary>
    /// Função de verificação se o player esta no range da planta
    /// </summary>
    /// <param name="distance">Distancia maxima do range da Plant</param>
    /// <returns>Retorna true se esta no range, caso contrario retorna false</returns>
    bool CanSeePlayer(float distance)
    {
        // Cria uma variavel e set o valor padrão dele como false.
        bool val = false;
        // Cria a variavel e atribui a ela a distancia maxima do agro da Plant
        float castDist = distance;

        // Verifica a direção do tiro
        if (!ShootToLeft)
        {
            // Se for false seta a distancia como negativa (verifica para a direita)
            castDist = -distance;
        }

        // Cria um ponto de referencia ficticio.
        Vector2 endPos = CastPoint.position + Vector3.left * castDist;

        // Utiliza o Raycast para criar um raio de verificação.
        RaycastHit2D hit = Physics2D.Linecast(CastPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        // Verifica se colidiu em algo
        if (hit.collider != null)
        {
            // Verifica se a colisão foi com o player.
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                // Se foi seta a variavel como true (vejo o Player)
                val = true;
            }
            else
            {
                // Senaõ seta a variavel como false (não vejo o Player)
                val = false;
            }
            // Desenha uma linha amarela para debug se teve colisão.
            Debug.DrawLine(CastPoint.position, hit.point, Color.yellow);
        }
        else
        {
            // Desenha uma linha azul para debug se não teve colisão.
            Debug.DrawLine(CastPoint.position, endPos, Color.blue);
        }
        // Retorna se ve ou não o Player.
        return val;
    }

    /// <summary>
    /// Função resposavel por instanciar o disparo.
    /// </summary>
    void spawaShoot()
    {
        // Função da Unity que instancia um GameObject.
        Instantiate(shootPrefab, CastPoint.position, Quaternion.identity);
    }
}
