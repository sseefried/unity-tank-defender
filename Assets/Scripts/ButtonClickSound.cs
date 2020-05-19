using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour, IPointerEnterHandler
{

    [SerializeField] AudioClip sound;

    /*
     * The declarations below are C#'s Auto-Implemented Properties
     * (aka Auto Properties).
     * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties
     */
    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source {  get { return GetComponent<AudioSource>(); } }

    // Start is called before the first frame update
    void Start()
    {
        // Programatically adding what is required. This is a technique
        // I will use from now on.
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
