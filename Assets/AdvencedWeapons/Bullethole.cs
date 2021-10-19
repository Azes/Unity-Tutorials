using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullethole : MonoBehaviour
{
    public Material material;
    public Texture2D[] textures;
    public AudioClip[] sounds;
    public AudioClip[] abprallSounds;
    public bool hitten;
    public int minDeleteTime;
    public int maxDeleteTime;


    void Start()
    {
        Material m = Instantiate(material);
        m.name = "copyMaterial";
        m.SetTexture("_mainTex", textures[Random.Range(0, textures.Length-1)]);
        GetComponent<Renderer>().material = m;

        GetComponent<AudioSource>().PlayOneShot((hitten ? sounds[Random.Range(0, sounds.Length - 1)]:
            abprallSounds[Random.Range(0, abprallSounds.Length - 1)]));

        Destroy(gameObject, Random.Range(minDeleteTime, maxDeleteTime));
    
    }

    
}
