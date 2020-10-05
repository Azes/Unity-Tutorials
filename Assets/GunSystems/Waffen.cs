using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waffen : MonoBehaviour
{

    
    public GameObject g_Ak;
    
    public GameObject g_M4A1;
    
    public GameObject g_Scorpiton;

    public int weaponSlot = 0;

    public List<string> _wl = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changeweapon();
        getWeapon();
    }

    void changeweapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") >= 0.1)
        {
            weaponSlot += 1;

            if (weaponSlot > 3) weaponSlot = 3;

        }
        else if (Input.GetAxis("Mouse ScrollWheel") <= -0.1)
        {
            weaponSlot -= 1;

            if (weaponSlot < 0) weaponSlot = 0;

        }

        if(weaponSlot > 0)
        {
            if(_wl.Count >= weaponSlot)
            {
                if (_wl[weaponSlot - 1].Equals("AK"))
                {
                    g_M4A1.SetActive(false);
                    g_Scorpiton.SetActive(false);
                    g_Ak.SetActive(true);
                }
                else if (_wl[weaponSlot - 1].Equals("M4A1"))
                {
                    g_M4A1.SetActive(true);
                    g_Scorpiton.SetActive(false);
                    g_Ak.SetActive(false);
                }
                else if (_wl[weaponSlot - 1].Equals("Scorption"))
                {
                    g_M4A1.SetActive(false);
                    g_Scorpiton.SetActive(true);
                    g_Ak.SetActive(false);
                }
            }
        }
        else
        {
            g_M4A1.SetActive(false);
            g_Scorpiton.SetActive(false);
            g_Ak.SetActive(false);
        }

    }

    void getWeapon()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            Ray r = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));

            if(Physics.Raycast(r, out hit, 20))
            {
                if(hit.collider.gameObject.transform.GetComponent<wID>() != null)
                {
                    if (hit.collider.gameObject.transform.GetComponent<wID>().w_Name.Equals("AK"))
                    {
                        _wl.Add("AK");
                        Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.transform.GetComponent<wID>().w_Name.Equals("M4A1"))
                    {
                        _wl.Add("M4A1");
                        Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.transform.GetComponent<wID>().w_Name.Equals("Scorption"))
                    {
                        _wl.Add("Scorption");
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
