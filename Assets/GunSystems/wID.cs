using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wID : MonoBehaviour
{
    public string w_Name;
    public float rotation;

    private void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * rotation, 0));
    }

}
