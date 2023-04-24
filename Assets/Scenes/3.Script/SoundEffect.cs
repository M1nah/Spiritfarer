using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    
    static AudioSource audioSource;
    public static AudioClip audioclip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioclip = Resources.Load<AudioClip>("SFX_Event_Meteor_Impact_06"); 
   
    }

public void SoundPlay()
    {
        audioSource.PlayOneShot(audioclip);
    }

}
