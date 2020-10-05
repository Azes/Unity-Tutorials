using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorT : MonoBehaviour
{
    public int side;
    public DoorM d;

    private void OnTriggerEnter(Collider other)
    {
        d.rote(side);
    }

    private void OnTriggerStay(Collider other)
    {
        d.rote(side);
    }

    private void OnTriggerExit(Collider c)
    {
        d.reset = true;
    }
}
