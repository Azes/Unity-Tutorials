using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorM : MonoBehaviour
{


    Quaternion normal;

    public bool reset;

    private void Start()
    {
        normal = transform.rotation;
    }

    public void rote(int side)
    {
        if(side == 1)
        {
            float f = 0;
            f += 200f * Time.deltaTime;
            transform.Rotate(0, 0, f);
        }
        else if (side == 2)
        {
            float f = 0;
            f -= 200f * Time.deltaTime;
            transform.Rotate(0, 0, f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (reset)
        {
            if (transform.rotation == normal) reset = false;

            transform.rotation = Quaternion.Lerp(transform.rotation, normal, 1.3f * Time.deltaTime);
        }
        
    }
}
