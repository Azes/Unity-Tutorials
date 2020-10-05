using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class munzenSpawn : MonoBehaviour
{

    public GameObject _coinPre;
    private GameObject _coi;
    public bool notTigger;

    private void OnTriggerEnter(Collider other)
    {
        notTigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        notTigger = false;   
    }

    // Update is called once per frame
    void Update()
    {

        if (_coi == null && !notTigger)
        {
            if(Random.value > 0.5f)
            {
                _coi = Instantiate(_coinPre, transform.position, Quaternion.identity);
            }
            notTigger = true;
        }
    }
}
