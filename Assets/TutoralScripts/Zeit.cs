using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeit : MonoBehaviour
{
    public GameObject spawn;
    public GameObject w;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Instantiate(w, spawn.transform.position, Quaternion.identity);

        if (Input.GetButtonDown("Fire2")) Time.timeScale = 0.25f;

        if (Input.GetButtonUp("Fire2")) Time.timeScale = 1f;

    }
}
