
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe responsavel pelo controle do player
/// </summary>
public class Player : MonoBehaviour
{

    [Header("Player movement")]
    [Tooltip("Player dislocation Speed")]
    public float Speed = 7.0f;

    [Tooltip("Force add on player to jump")]
    public float JumpForce = 17.0f;

    [Tooltip("Force add on player kill enimies")]
    public float JumpFeedback = 15.0f;

    [Tooltip("Player HP")]
    public int health = 2;

    [Tooltip("Jumping Sound")]
    public AudioSource audioSource;

    /// <summary>
    /// Player Rigidbody
    /// To use on jump, demage,and others.
    /// </summary>
    private Rigidbody2D rig;

    /// <summary>
    /// Player animator
    /// To use on jump, demage,and others animations.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Metodo default do unity, executa antes das demais funções.
    /// </summary>
    public void Start()
    {
        // Get player reigebody
        rig = GetComponent<Rigidbody2D>();
        // Get player animator
        animator = GetComponent<Animator>();
        // Get audio sourcecomponent
        audioSource = GetComponent<AudioSource>();
        // Call function to respawn in specific local
        IsReSpaw();
    }

    /// <summary>
    /// Metodo default do unity, executa a cada x milisegundos com base na maquina que esta rodando.
    /// </summary>
    public void Update()
    {
        // Execua a função de mover o player.
        Move();
        // Execua a função de pular do player.
        Jump();
        // Verifica se o player esta pulando.
        IsJumping();
    }

    /// <summary>
    /// Metodo de movimentação do player.
    /// </summary>
  public  void Move()
    {
        // Pega a direção de movimentação do player.
        Vector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        // Movimenta o player.
        transform.position += moviment * Time.deltaTime * Speed;
        // Verifica se a direção do player esta pra esquerda.
        if (Input.GetAxis("Horizontal") < 0)
        {
            // Muda a direção do sprite do player.
            transform.rotation = Quaternion.Euler(0.0f, 180f, 0);
        }
        // Verifica se a direção do player esta pra direita.
        if (Input.GetAxis("Horizontal") > 0)
        {
            // Muda a direção do sprite do player.
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        // Verifica se o player esta se movendo.
        if (Input.GetAxis("Horizontal") != 0)
        {
            // Seta a animação de runing do player.
            animator.SetBool("isRuning", true);
        }
        else
        {
            // Seta a animação de idle do player.
            animator.SetBool("isRuning", false);
        }
    }

    /// <summary>
    /// Metodo de pulo do player.
    /// </summary>
    public void Jump()
    {
        // Verifica se a barra de espaço foi presionada e a valocidade do eixo Y é 0.
        if (Input.GetButtonDown("Jump") && rig.velocity.y == 0)
        {
            // Addicia força de pulo no player no modo impulso.
            rig.AddForce(new Vector2(0.0f, JumpForce), ForceMode2D.Impulse);
            // Executa o audio de pulo do player.
            playAudio();
        }
    }

    /// <summary>
    /// Metodo de pulo do player.
    /// </summary>
    public void IsJumping()
    {
        // Verifica se a valocidade no eixo Y é maior que 0.
        if (rig.velocity.y > 0)
        {
            // Seta a animiação de pulo no player.
            animator.SetBool("IsJumping", true);
            // Desabilita a animação de queda do player.
            animator.SetBool("IsFalling", false);
        }
        // Verifica se a valocidade no eixo Y é menor que 0.
        else if (rig.velocity.y < 0)
        {
            // Desabilita a animiação de pulo no player.
            animator.SetBool("IsJumping", false);
            // Seta a animação de queda do player.
            animator.SetBool("IsFalling", true);
        }
        else
        {
            // Desabilita a animiação de pulo no player.
            animator.SetBool("IsJumping", false);
            // Desabilita a animação de queda do player.
            animator.SetBool("IsFalling", false);
        }
    }

    /// <summary>
    /// Metodo de feedback quando o player toma dano.
    /// </summary>
    public void KillFeedback()
    {
        // Addiciona força de feedback no player no modo impulso.
        rig.AddForce(new Vector2(0.0f, JumpFeedback), ForceMode2D.Impulse);
        // Executa o audio de pulo do player.
        playAudio();
    }

    /// <summary>
    /// Metodo de feedback quando o player toma dano.
    /// </summary>
    public void playAudio()
    {
        // Executa o audio de pulo do player.
        audioSource.Play();
    }

    /// <summary>
    /// Metodo de responsavel pela morte do player.
    /// </summary>
    public void  KillPlayer()
    {
        // Remove 1 de life do player.
        health -= 1;
        // Executa a animação de "toma dano".
        animator.Play("Frog_hit");
        // Verifica se a vida é menor ou igual a 0.
        if (health <= 0)
        {
            // Destroy o GameObject do player.
            Destroy(this.gameObject);
            // Carrega a cena de GameOver.
            SceneManager.LoadScene("GameOver");
        }
    }

    /// <summary>
    /// Metodo responsavel pelo respawn apos a visualização de um anuncio com recompensa.
    /// </summary>
    void IsReSpaw()
    {
        // Pega o nome da ultima cena que estava.
        string respaw = PlayerPrefs.GetString("respaw");
        // Verifica se o retorno é diferente de vazio.
        if("" != respaw)
        {
            // Busca o ponto de spawn pre definido.
            this.gameObject.transform.position = GameObject.Find("ReSpawPoint").transform.position;
            // Deleta a flag do respawn.
            PlayerPrefs.DeleteKey("respaw");
        }
    }
}