using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hock : MonoBehaviour
{

    //bei benutzung eines charaktercontroller
    public CharacterController ch;
    public Rigidbody rig;
    public LineRenderer line;
    public GameObject linespawn;
    public float power;
    public float minDis = 2;
    private bool istarget;
    private Vector3 target;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonUp("Fire1"))
        {

            rig.useGravity = true;
            istarget = false;
            ch.enabled = true;
            line.transform.gameObject.SetActive(false);
            rig.velocity = Vector3.zero;
            target = Vector3.zero;
        }
        else if (Input.GetButton("Fire1") && !istarget)
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
            if (Physics.Raycast(r, out hit, Mathf.Infinity))
            {
                ch.enabled = false;
                rig.isKinematic = false;
                target = hit.point;
                line.transform.gameObject.SetActive(true);
                line.SetPosition(0, linespawn.transform.position);
                line.SetPosition(1, target);
                istarget = true;
                
            }
            else istarget = false;
        }

        


        if (istarget)
        {
            line.SetPosition(0, linespawn.transform.position);
            line.SetPosition(1, target);
            //wenn char.conrtoller;

                rig.velocity = Vector3.zero;
                dir = (-(transform.position - target).normalized) * power;
                dir.y += 0.1f;
                if((transform.position - target).sqrMagnitude >= (minDis * minDis))rig.AddForce(dir , ForceMode.Impulse);
            
         
        }
        else if (ch.isGrounded)
        {
            rig.isKinematic = true;
        }
    }
    
  
}
