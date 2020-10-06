using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleAI : MonoBehaviour
{

    public int Health = 100;
    public bool isDead;
    public float moveSpeed = 50;
    public float aggroRange = 10;
    public GameObject player;
    public Rigidbody r;
    public bool isAtk;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FPSController");
        r = GetComponent<Rigidbody>();
    }

   
    public void addDamage(int damage)
    {
        Health -= damage; 

        if(Health <= 0)
        {
            isDead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {



            float dis = Vector3.Distance(transform.position, player.transform.position);
            if(isAtk || dis <= aggroRange)
            {
                transform.LookAt(player.transform);

                r.AddForce(transform.forward * moveSpeed * Time.deltaTime, ForceMode.Force);
            }
        }
        else Destroy(gameObject);
    }
}
