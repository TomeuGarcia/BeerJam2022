using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public GameObject audioObject;

    public void DropAudio() 
    {
        Instantiate(audioObject, transform.position, transform.rotation).GetComponent<AudioSource>().pitch = 
            Random.Range(0.85f, 1.1f);
    }
}
