using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannot : MonoBehaviour
{
    public btrigger b;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("spawner")) b.can = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("spawner"))b.can = true;
        Debug.Log("exit " + other.gameObject);
    }
}
