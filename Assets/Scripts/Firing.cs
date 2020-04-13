using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    [SerializeField] AudioClip firingSound;
    [Range(0, 1)] [SerializeField] float volume = 0.5f;

    public void PlayFireSound()
    {
        AudioSource.PlayClipAtPoint(firingSound, Camera.main.transform.position, volume);
    }
    
}
