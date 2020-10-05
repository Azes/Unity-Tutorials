using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("arrow")) return;
        transform.parent.GetComponent<Rigidbody>().Sleep();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
