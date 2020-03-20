using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    [SerializeField] AudioClip firingSound;

    public void PlayFireSound()
    {
        AudioSource.PlayClipAtPoint(firingSound, Camera.main.transform.position);
    }
    
}
