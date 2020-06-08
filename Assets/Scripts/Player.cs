using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player movement")]
    [Tooltip("Player dislocation Speed")]
    public float Speed = 7.0f;

    [Tooltip("Force add on playr to jump")]
    public float JumpForce = 10.0f;

    /// <summary>
    /// Player Rigidbody
    /// To use on jump, demage,and others.
    /// </summary>
    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        transform.position += moviment * Time.deltaTime * Speed;
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180f,0);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rig.AddForce(new Vector2(0.0f, JumpForce), ForceMode2D.Impulse);
        }
    }
}
