using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterGun : MonoBehaviour
{
    public int Damage, Ammo, MagazinSize, CurrentAmmo;
    public float fireRate, shootRange;

    float fireCoolDown;

    public Camera cam;

    public GameObject ShootSound, BulletHole, muzzle;
    [Space(5)]
    public AudioSource audioSource;
    public AudioClip[] sounds;


    [Space(15)]
    [Header("Better Stuff")]
    public bool AIM,BulletDrop;
    public Vector2 normalSpreadMinMax;
    public GameObject kugel;
    public float Power;
    //public float shootAngle;
    //public AudioClip[] angleSounds;

    //[Header("pumgun")]
    bool isPump;
    int schrots;

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

        AIM = Input.GetButton("Fire2");

        bool fire = Input.GetButton("Fire1");

        if (fire && fireCoolDown <= 0 && CurrentAmmo > 0)
        {
            if (!muzzle.activeInHierarchy) muzzle.SetActive(true);

            fireCoolDown = 1;
            CurrentAmmo--;

            Instantiate(ShootSound, transform.position, Quaternion.identity);

            for (int i = 0; i < (isPump ? schrots : 1); i++)
            {


                var pos = new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane);
                if (!AIM || isPump)
                {
                    pos.x += Random.insideUnitSphere.x *
                        Random.Range(normalSpreadMinMax.x, normalSpreadMinMax.y) * 10;

                    pos.y += Random.insideUnitSphere.y *
                         Random.Range(normalSpreadMinMax.x, normalSpreadMinMax.y) * 10;
                }
                Ray r = cam.ScreenPointToRay(pos);

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

                    if (BulletDrop && AIM && !isPump)
                    {

                        if (AIM) pos = cam.transform.forward;
                        Instantiate(kugel, r.GetPoint(shootRange), Quaternion.LookRotation(cam.transform.forward)).GetComponent<Rigidbody>().AddForce(pos * Power, ForceMode.VelocityChange);
                        Debug.Break();
                    }

                    ///debug
                    end = r.GetPoint(shootRange);
                    ///
                }

                ///debug
                if (DebugMode) Debug.DrawLine(transform.position, end, Color.red);
                ///
            }
        }
        else
        {
            if (!fire && muzzle.activeInHierarchy) muzzle.SetActive(false);
            if (fire && CurrentAmmo <= 0)
            {
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
