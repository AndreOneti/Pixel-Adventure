using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    public bool IsLeft = true;

    public float Speed;

    public float MaxDistance;

    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

    void ShootDestroy()
    {
        float currentDistance = Vector3.Distance(startingPosition, transform.position);
        if (currentDistance > MaxDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
