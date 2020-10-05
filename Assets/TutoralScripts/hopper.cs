using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class hopper : MonoBehaviour
{

    public FirstPersonController fps;
    public CharacterController ch;
    public Rigidbody ri;

    public float force;
    public float zView = 30;

    bool isWall;
    bool isIt;
    public bool isnotWall;

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("wall"))
        {
            isWall = true;
            isnotWall = true;
        }
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.CompareTag("wall"))
        {
            isnotWall = true;
        }
        
    }


    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("wall"))
        {
            isnotWall = false;
        }
        isWall = false;
    }

    // Update is called once per frame
    void Update()
    {
    
            
        if(isWall)
        {
            if (!isIt)
            {
                isIt = true;
                fps.enabled = false;
                ch.enabled = false;
                ri.isKinematic = false;
                transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -zView));
               
            }

            ri.AddForce(transform.forward + Vector3.up * force , ForceMode.Impulse);
        }
        else
        {
            if (isIt)
            {
                ri.isKinematic = true;
                transform.rotation *= Quaternion.Euler(new Vector3(0, 0, zView));
                isIt = false;
                fps.enabled = true;
                ch.enabled = true;
            }

        }

    }
}
