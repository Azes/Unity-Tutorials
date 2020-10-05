using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bogen : MonoBehaviour
{

    public GameObject arrow;
    public Transform spawn;
    public float power;
    public float span_speed;
    public float span;
    private bool spannen;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1"))
        {
            spannen = true;

            if (span < power)
            {
                span += Time.deltaTime * span_speed;
            }
        }
        else
        {
            if (spannen)
            {
                GameObject g = Instantiate(arrow, spawn.position, spawn.rotation);
                Rigidbody r = g.GetComponent<Rigidbody>();
                r.AddForce(Camera.main.transform.forward * span);
                spannen = false;
                span = 0;
            }
        }
        
    }
}
