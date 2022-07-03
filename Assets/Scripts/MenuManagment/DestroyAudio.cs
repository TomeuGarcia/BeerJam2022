using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAudio : MonoBehaviour
{
    [SerializeField] AudioSource audio;

    void Start()
    {
        Destroy(audio, 3f);
    }

}
