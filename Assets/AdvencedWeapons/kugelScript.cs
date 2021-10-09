using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kugelScript : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 20);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
