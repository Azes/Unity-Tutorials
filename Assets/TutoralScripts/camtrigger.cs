using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camtrigger : MonoBehaviour
{
    public Material _m;

    private void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody.transform.CompareTag("Player"))
        {
            _m.color = Color.red;
        }
    }


    private void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody.transform.CompareTag("Player"))
        {
            _m.color = Color.gray;
        }
    }


}
