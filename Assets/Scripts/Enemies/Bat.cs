using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

     void Start()
    {
        origin = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moviment = new Vector3(0.0f, 1.0f, 0.0f);
        if (isUP)
        {
            transform.position += moviment * Time.deltaTime * speed;
        }
        else
        {
            transform.position += moviment * Time.deltaTime * - speed;
        }
        Limite();
    }

    private void Limite()
    {
        if (Vector3.Distance(origin, transform.position) > deslocamentoY)
        {
            isUP = !isUP;
        }
    }
}
