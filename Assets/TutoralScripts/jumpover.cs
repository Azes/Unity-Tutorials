using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class jumpover : MonoBehaviour
{

    public FirstPersonController fps;
    public CharacterController ch;
    public Rigidbody ri;

    public float force;
    public float angle;

    bool isWall, itis;
    float t;
    Quaternion rot;


    private void OnTriggerEnter(Collider c)
    {

        if (c.CompareTag("wall"))
        {
            isWall = true;
        }
        
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.CompareTag("wall"))
        {
            isWall = true;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("wall"))
        {
            isWall = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isWall)
        {

            if (!itis)
            {
                itis = true;
                fps.enabled = false;
                ch.enabled = false;
                ri.isKinematic = false;
                transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -angle));

                rot = transform.rotation;
                t = 0;
            }


            ri.AddForce(transform.forward * force, ForceMode.Impulse);

        }
        else
        {

            if (itis)
            {
                t += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, rot * Quaternion.Euler(new Vector3(0, 0, angle)), t);

                if(transform.rotation.Equals(rot * Quaternion.Euler(new Vector3(0, 0, angle))))
                {

                    itis = false;
                    fps.enabled = true;
                    ch.enabled = true;
                    ri.isKinematic = true;
                }

            }


        }
    }
}
