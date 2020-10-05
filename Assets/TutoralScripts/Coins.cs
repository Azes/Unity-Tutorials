using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public float _rotation;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * _rotation, 0));    
    }

    private void OnTriggerEnter(Collider other)
    {
        MIniCar.coins++;
        Destroy(gameObject);
    }

}
