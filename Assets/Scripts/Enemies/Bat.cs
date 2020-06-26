using UnityEngine;

/// <summary>
/// Classe responsavel por maipular o BAT.
/// </summary>
public class Bat : MonoBehaviour
{
    [Header("Bat movement variable")]
    [Tooltip("Deslocamento no eixo Y do Bat")]
    public float deslocamentoY = 3.0f;

    [Tooltip("Velocidade do Deslocamento do Bat")]
    public float speed = 1.0f;

    [Tooltip("Verfica se esta subindo")]
    public bool isUP;

    /// <summary>
    /// Posicao Incial do Bat.
    /// </summary>
    [SerializeField]
    [Tooltip("Posicao Inicial do Bat")]
    private Vector3 origin;

    /// <summary>
    /// Metodo default do unity, executa antes das demais funções.
    /// </summary>
    void Start()
    {
        // Pega a posição inicial do BAT.
        origin = this.gameObject.transform.position;
    }

    /// <summary>
    /// Metodo default do unity, executa a cada x milisegundos com base na maquina que esta rodando.
    /// </summary>
    void Update()
    {
        // Chama a função de movimentação do BAT.
        Move();
        // Chama a função de troca de direção do movimento.
        Limite();
    }

    /// <summary>
    /// Função responsavel pela movimentação do BAT.
    /// </summary>
    private void Move()
    {
        // Seta um Vector3 para movimentar o Bat.
        Vector3 moviment = new Vector3(0.0f, 1.0f, 0.0f);
        // Verifica se ele esta subindo.
        if (isUP)
        {
            // Seta a velocidade de movimentação como positiva(aumentando no eixo Y).
            // E normaliza a velocidade com o Time.deltaTime.
            transform.position += moviment * Time.deltaTime * speed;
        }
        else
        {
            // Seta a velocidade de movimentação como negativa(decremento no eixo Y).
            // E normaliza a velocidade com o Time.deltaTime.
            transform.position += moviment * Time.deltaTime * -speed;
        }
    }

    /// <summary>
    /// Função responsavel pela verificação se o BAT chegou na distancia maxima referente ao ponto de origem.
    /// </summary>
    private void Limite()
    {
        // Verificação do deslocamento do BAT no eixo Y.
        // Se chegou na distancia maxima referente ao ponto de origem.
        if (Vector3.Distance(origin, transform.position) > deslocamentoY)
        {
            // Inverte a direção do movimento do BAT.
            isUP = !isUP;
        }
    }
}
