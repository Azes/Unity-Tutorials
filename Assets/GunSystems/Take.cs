using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take : MonoBehaviour
{

    public GameObject place;
    private GameObject item;
    private RaycastHit hit;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) taken();
        else if (Input.GetButtonUp("Fire1")) fallen();
    }


    void taken()
    {
        var pos = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);

        if(Physics.Raycast(Camera.main.ScreenPointToRay(pos), out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("item"))
            {
                item = hit.collider.gameObject;
                item.GetComponent<Rigidbody>().isKinematic = true;
                item.transform.parent = place.transform;
            }
        }
    }

    void fallen()
    {
        if(item != null)
        {
            item.transform.parent = place.transform.root.parent;
            item.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

}
