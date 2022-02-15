using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 0.15f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 bpos = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            bpos.x += speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            bpos.x -= speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            bpos.z += speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            bpos.z -= speed;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = bpos * 1.2f;
        }
        else
        {
            rb.velocity = bpos;
        }
    }
}
