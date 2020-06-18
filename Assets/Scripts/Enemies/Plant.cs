using UnityEngine;

public class Plant : MonoBehaviour
{

    public Transform player;

    public float agroRange;

    private Rigidbody2D rb2d;

    public Transform CastPoint;

    public bool PlayerIsLeft = true;

    public float timeToShoot;

    public GameObject shootPrefab;

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        shoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.SendMessage("KillPlayer", SendMessageOptions.DontRequireReceiver);
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void shoot()
    {
        if (CanSeePlayer(agroRange))
        {
            //Debug.Log("Vejo o player");
            if (currentTime >= timeToShoot)
            {
                Debug.Log("Shoot in enimy");
                spawaShoot();
                currentTime = 0;
            }
        }
        else
        {
            //Debug.Log("Não vejo o player");
            if (currentTime >= timeToShoot)
            {
                currentTime = 0;
            }
        }
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (!PlayerIsLeft)
        {
            castDist = -distance;
        }

        Vector2 endPos = CastPoint.position + Vector3.left * castDist;
    
        RaycastHit2D hit = Physics2D.Linecast(CastPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(CastPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(CastPoint.position, endPos, Color.blue);
        }
        return val;
    }

    void spawaShoot()
    {
        Instantiate(shootPrefab, CastPoint.position, Quaternion.identity);
    }
}
