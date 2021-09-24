using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGun : MonoBehaviour
{
    public int Damage, Ammo, MagazinSize, CurrentAmmo;
    public float fireRate, shootRange;

    float fireCoolDown;

    public Camera cam;
    
    public GameObject ShootSound, BulletHole, muzzle;
    [Space(5)]
    public AudioSource audioSource;
    public AudioClip[] sounds;

    RaycastHit hit;


    ///debug
    [Space(10)]
    public bool DebugMode;
    Vector3 end = Vector3.zero;
    ///
    void Start()
    {
        
    }

    void Update()
    {

        bool fire = Input.GetButton("Fire1");

        if (fire && fireCoolDown <= 0 && CurrentAmmo > 0)
        {
            if (!muzzle.activeInHierarchy) muzzle.SetActive(true);

            fireCoolDown = 1;
            CurrentAmmo--;

            Instantiate(ShootSound, transform.position, Quaternion.identity);

            Ray r = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane));

            if (Physics.Raycast(r, out hit, shootRange))
            {
                ///debug
                end = hit.point;
                ///

                if (hit.collider != null)
                {
                    Instantiate(BulletHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

                    if (hit.collider.CompareTag("Enemy")) hit.collider.GetComponent<SimpleEnemy>().setDamage(Damage);
                }
            }
            else
            {
                ///debug
                end = r.GetPoint(shootRange);
                ///
            }

            ///debug
            if (DebugMode) Debug.DrawLine(transform.position, end, Color.red);
            ///

        }
        else
        {
            if (!fire && muzzle.activeInHierarchy) muzzle.SetActive(false);
            if (fire && CurrentAmmo <= 0)
            {
                muzzle.SetActive(false);
                if (!audioSource.isPlaying) audioSource.PlayOneShot(sounds[0]);
            }
        }



        if (fireCoolDown > 0) fireCoolDown -= Time.deltaTime * fireRate;

        //Reload
        if (CurrentAmmo <= 0 && Ammo > 0 && Input.GetKeyDown(KeyCode.R))
        {
            audioSource.PlayOneShot(sounds[1]);

            Ammo -= MagazinSize;

            if (Ammo < 0)
            {
                CurrentAmmo = MagazinSize - Mathf.Abs(Ammo);
                Ammo = 0;
            }
            else CurrentAmmo = MagazinSize;

        }

    }
}
