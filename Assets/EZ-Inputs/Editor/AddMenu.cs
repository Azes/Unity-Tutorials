using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddMenu
{

    [MenuItem("AzeS/Inport Ez Input")]
    public static void addAc()
    {
        if (GameObject.Find("EZ-Input") == null)
        {
            GameObject g = new GameObject("EZ-Input");
            g.AddComponent<AC>();
        }
        else Debug.LogError("EZ-Inputs is Aktive");
    }
}
