using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayShootSound : MonoBehaviour
{
    public AudioSource au;
    public AudioClip[] soundfiles;
    
    void Start()
    {
        au.PlayOneShot(soundfiles[Random.Range(0, soundfiles.Length - 1)]);
        Destroy(gameObject, 5);
        Debug.Log("Played");
    }

}
