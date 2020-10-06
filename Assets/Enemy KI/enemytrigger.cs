using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytrigger : MonoBehaviour
{

    public simpleAI s;

    private void Start()
    {
        s = transform.parent.GetComponent<simpleAI>();
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            RaycastHit hit;

            Vector3 dir = s.player.transform.position - transform.position;

            if(Physics.Raycast(transform.position, dir, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("jo");
                    s.isAtk = true;
                }
                else s.isAtk = false;
            }

           
        }
    }

    private void OnTriggerExit(Collider c)
    {
       
    }
}
