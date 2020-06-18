using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.SendMessage("KillPlayer", SendMessageOptions.DontRequireReceiver);
        }
    }

    void Move()
    {

        transform.Translate(Vector2.left * Speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= moveTime)
        {
            isRight = !isRight;
            timer = 0.0f;
            if (isRight)
            {
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
