using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIN : MonoBehaviour
{

    public GameObject input;
    bool console;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            console = !console;
            input.SetActive(console);
            ConsoleSystem.menuAktive = console;
        }
    }
}
