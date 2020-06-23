
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [Header("Player movement")]
    [Tooltip("Player dislocation Speed")]
    public float Speed = 7.0f;

    [Tooltip("Force add on playr to jump")]
    public float JumpForce = 17.0f;

    [Tooltip("Force add on playr kill enimies")]
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

    // Start is called before the first frame update
  public void Start()
    {
        // Get player reigebody
        rig = GetComponent<Rigidbody2D>();
        // Get player animator
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
  public  void Update()
    {
        Move();
        Jump();
        IsJumping();
    }

  public  void Move()
    {
        Vector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        transform.position += moviment * Time.deltaTime * Speed;
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180f, 0);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("isRuning", true);
        }
        else
        {
            animator.SetBool("isRuning", false);
        }
    }

  public void Jump()
    {
        if (Input.GetButtonDown("Jump") && rig.velocity.y == 0)
        {
            rig.AddForce(new Vector2(0.0f, JumpForce), ForceMode2D.Impulse);
            playAudio();
        }
    }

   public void IsJumping()
    {
        if (rig.velocity.y > 0)
        {
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", false);
        }
        else if (rig.velocity.y < 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
    }

    public void KillFeedback()
    {
        rig.AddForce(new Vector2(0.0f, JumpFeedback), ForceMode2D.Impulse);
        playAudio();
    }

    public void playAudio()
    {
        audioSource.Play();
    }

    public void  KillPlayer()
    {
        health -= 1;
        animator.Play("Frog_hit");
        if (health <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
}