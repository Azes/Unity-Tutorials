using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Store : MonoBehaviour
{
    public FirstPersonController fps;

    public Weapons _w;

    public GameObject window;

    public Text _coinValue;

    bool isopen;

    public void OpenUi()
    {
        isopen = true;
        window.SetActive(true);
        fps.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void getAK()
    {

        if(_w.COINS >= 1000)
        {
            _w.COINS -= 1000;
            _w.setWeapon("AK");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isopen)
        {
            if(!_coinValue.text.Equals("Coins : " + _w.COINS.ToString())) _coinValue.text = "Coins : " + _w.COINS.ToString();


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                window.SetActive(false);
                fps.enabled = true;
                isopen = false;
                Cursor.lockState = CursorLockMode.Locked;
            }




        }
    }
}
