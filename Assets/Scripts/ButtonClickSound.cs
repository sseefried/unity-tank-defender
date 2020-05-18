using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour, IPointerEnterHandler
{

    [SerializeField] AudioClip sound;

    // TODO: Find out what is going on here. What feature of C# is this? 
    private Button button {  get { return GetComponent<Button>(); } }
    private AudioSource source {  get { return GetComponent<AudioSource>(); } }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;


            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound();
    }

    void PlaySound()
    {
        source.PlayOneShot(sound);
    }

}
