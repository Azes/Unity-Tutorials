using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btrigger : MonoBehaviour
{

    public buildsystem b;
    public Material _m;
    public Color _normal, _cannot;
    public bool can = true;
    private void OnTriggerEnter(Collider other)
    {
        b.canBuild = can ? true : false;
        _m.color = _normal;

    }

    private void OnTriggerStay(Collider other)
    {
        b.canBuild = can ? true : false;


    }

    private void OnTriggerExit(Collider other)
    {
        b.canBuild = false;
        _m.color = _cannot;
    }

    private void LateUpdate()
    {
        if (!can || !b.canBuild) _m.color = _cannot;
        else
        {
            if(_m.color != _normal && b.canBuild)_m.color = _normal;
        }
    }
}
