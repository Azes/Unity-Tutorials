using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject hole;
    public GameObject sound;
    public ParticleSystem muzzel;

    public Animator _ani;
    public float normal_spread, aim_spread;
    public int Ammo, Magazin, CurrentAmmo;
    bool AIM;

    [Space(5)]

    public float shootspeed;
    private float waittime;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        shoot();
    }


    void shoot()
    {

        if (Input.GetButtonDown("Fire2"))
        {
            AIM = !AIM;
            _ani.SetBool("aim", AIM);
        }

        if(Input.GetButton("Fire1") && waittime <= 0 && CurrentAmmo > 0)
        {
            waittime = 1;

            if (!muzzel.isPlaying) muzzel.Play();

            var pos = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);

            if (AIM)
            {
                pos.x += RandPoint(1, 2).x * aim_spread;
                pos.y += RandPoint(1, 2).y * aim_spread;
            }
            else
            {
                pos.x += RandPoint(1, 2).x * normal_spread;
                pos.y += RandPoint(1, 2).y * normal_spread;

            }

            CurrentAmmo--;
            Instantiate(sound);

            if(Physics.Raycast(Camera.main.ScreenPointToRay(pos), out hit, Mathf.Infinity))
            {
                Instantiate(hole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

                if (hit.collider.transform.CompareTag("enemy")) Destroy(hit.collider.gameObject, 1);
            }
        }
        else
        {
            if (muzzel.isPlaying) muzzel.Stop();
        }
        
        waittime -= Time.deltaTime * shootspeed;

        if (waittime <= 0) waittime = 0;


        if(CurrentAmmo <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            if(Ammo > 0)
            {
                int i = 0;

                for(int x = 0; x < Magazin; x++)
                {
                    if (Ammo - x > 0) i++;
                    else break;
                }

                Ammo -= i;
                CurrentAmmo += i;
            }
        }

    }


    public Vector2 RandPoint(float min, float max)
    {
        Vector2 ru = new Vector2();
        Vector2 er = Random.insideUnitSphere * (4 * Random.Range(min, max));
        Vector2 zw = Random.insideUnitSphere * (4 * Random.Range(min, max));

        if(Random.value >= 0.5f)
        {
            ru.x = er.x + zw.y;
            ru.y = er.y + zw.x;
            return ru;
        }
        else
        {
            ru.x = zw.x - er.y;
            ru.y = zw.y - er.x;
            return ru;
        }

    }


}
