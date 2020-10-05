using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klettern : MonoBehaviour
{

    bool clamp;
    public float clamp_speed;
    public Rigidbody r;
    public CharacterController ch;


    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("ladder"))
        {
            clamp = true;
            r.useGravity = false;
            ch.enabled = false;
        }
    }


    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("ladder"))
        {
            clamp = false;
            r.useGravity = true;
            ch.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (clamp)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position += new Vector3(0, clamp_speed * Time.deltaTime, 0);
            }
        }

    }
}
