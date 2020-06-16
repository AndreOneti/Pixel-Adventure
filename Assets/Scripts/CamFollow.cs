using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    private Transform player;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        temp.x = player.position.x;
        temp.x += offset;
        transform.position = temp;
    }
}
