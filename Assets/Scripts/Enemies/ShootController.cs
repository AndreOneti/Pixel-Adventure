using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ShootController : MonoBehaviour
{

    public bool IsLeft = true;

    public float Speed;

    public float MaxDistance;

    Vector3 startingPosition;

    public GameObject BuletDestroy;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ShootDestroy();
    }

    public void Move()
    {
        if (IsLeft)
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * -Speed * Time.deltaTime);
        }
    }

    public void ShootDestroy()
    {
        float currentDistance = Vector3.Distance(startingPosition, transform.position);
        if (currentDistance > MaxDistance)
        {
            Explode();
        }
        else
        {
            Move();
        }
    }

    public void Explode()
    {
        if (BuletDestroy != null)
        {
            var partciles = Instantiate(BuletDestroy, transform.position, Quaternion.identity);
            Destroy(partciles, 1.5f);
        }
        Destroy(this.gameObject, 0.08f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explode();
            collision.gameObject.GetComponent<Player>().KillPlayer();
        }

    }
}
