using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAllControll : MonoBehaviour
{
    public float speed;
    public float jump;
    private Rigidbody rb;

    public bool ground;
    
    private void OnCollisionStay(Collision collision)
    {
        ground = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        ground = false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        float offX = Input.GetAxis("Horizontal");
        float offY = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(offX, 0, offY);

        if (ground && Input.GetButton("Jump"))
        {
            move.y += jump;
        }

        rb.AddForce(move * speed);

    }

}
