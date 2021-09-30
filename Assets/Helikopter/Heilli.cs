using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heilli : MonoBehaviour
{
    
    public float rotspeed, degreesSpeed, gravity;
    public Vector3 flySpeed;
    
    public float fadeoutSpeed;
    public Animator ani;
    public Transform camera;
    public float cameraSpeed;
    bool grounded;
    bool tfly;
    bool stopMove;
    bool ab;
    Vector3 forc,rotation;

    Rigidbody rig;


    private void Start()
    {
        rig = transform.parent.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        grounded = true;
        forc.y = 0;
        rig.useGravity = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        grounded = false;
    }

    public void setFly()
    {
        ani.SetBool("start", false);
        ani.SetBool("fly", true);
    }

    void flying()
    {
        
        rotation = Vector3.zero;
        ab = ani.GetBool("fly");

        
        if (Input.GetKey(KeyCode.E))
        {
            tfly = true;
            rig.useGravity = false;

            if (grounded && !ab) ani.SetBool("start", true);

            if (ab) forc.y = 1 * flySpeed.y;


        }
        else if (Input.GetKey(KeyCode.Q))
        {
            tfly = true;
           
            if (!grounded) forc.y = -1 * flySpeed.y;
        }
        else
        {
            if (!grounded) forc.y = -1 * gravity * Time.deltaTime;
            tfly = false;
        }


        if(!tfly)
        {
            if (ab && grounded)
            {
                ani.SetBool("start", false);
                ani.SetBool("fly", false);
            }
        }

        if (ab)
        {
            if (Input.GetKey(KeyCode.W))
            {
                stopMove = false;
                forc.z = 1 * flySpeed.z;
                rotation.x = 20;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                stopMove = false;
                forc.z = -1 * flySpeed.z;
                rotation.x = -20;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                forc.x = -1 * flySpeed.x ;
                rotation.z = 15;
                stopMove = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                forc.x = 1 * flySpeed.x ;
                rotation.z = -15;
                stopMove = false;
            }
            
            if(!Input.anyKey) stopMove = true;


            cameraMovement();
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotation), Time.deltaTime * degreesSpeed);

        }

        if (transform.localRotation != Quaternion.Euler(0, 0, 0))
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotation), Time.deltaTime * degreesSpeed);
        }

    }

    

    void cameraMovement()
    {
        float dy = Input.GetAxis("Mouse X");
        dy *= cameraSpeed * Time.deltaTime;
        camera.Rotate(0, dy, 0);

        transform.parent.rotation = 
            Quaternion.Lerp(transform.parent.rotation, camera.rotation, Time.deltaTime * rotspeed);
        camera.localRotation = Quaternion.Lerp(camera.localRotation, Quaternion.Euler(Vector3.zero), Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        flying();
    }

    private void FixedUpdate()
    {
        if (ab)
        {
            rig.velocity = Vector3.zero;

            if (stopMove) forc = Vector3.Slerp(forc, Vector3.zero, Time.fixedDeltaTime * fadeoutSpeed);

            rig.AddRelativeForce(forc * 10);
        }
    }
}
