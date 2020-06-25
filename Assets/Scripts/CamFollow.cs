using UnityEngine;

/// <summary>
/// Classe responsavel pela camera
/// </summary>
public class CamFollow : MonoBehaviour
{
    /// <summary>
    /// Referencia do player.
    /// </summary>
    private GameObject player;

    /// <summary>
    /// Metodo default do unity, executa antes das demais funções.
    /// </summary>
    void Start()
    {
        // Pega areferencia do player no jogo.
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Metodo default do unity, executa a cada x milisegundos com base na maquina que esta rodando.
    /// </summary>
    void Update()
    {
        // Verifica se a referencia do player é não nula.
        if (player)
        {
            // Cria a variavel temporaria coma posição da camera.
            Vector3 temp = transform.position;
            // Seta a posição em X da camera igual a do player.
            temp.x = player.transform.position.x;
            // Seta a posição na camera.
            transform.position = temp;
        }
    }
}
