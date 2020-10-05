using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{

    public GameObject _spawn;

    private void OnTriggerEnter(Collider c)
    {
        var p = _spawn.transform.position;
        p.z += 2;
        c.attachedRigidbody.transform.SetPositionAndRotation(p, Quaternion.identity);

    }

}
