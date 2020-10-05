using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int Coins = 0;
    public int _coins;

    // Update is called once per frame
    void Update()
    {
        _coins = Coins;
    }
}
