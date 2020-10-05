using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public int processTime;
    public int process;
    public int processSpeed;

    [Space(5)]
    public Image _bar;

    // Update is called once per frame
    void Update()
    {
        process += processSpeed;

        _bar.fillAmount = zeroToOne(process, processTime);

    }

    float zeroToOne(float value, float endvalue)
    {
        int i = (int)System.Math.Round((value / endvalue) * 100);
        float ii = i / 100.0f;
        return ii;
    }

}
