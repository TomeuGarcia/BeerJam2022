using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PickUp : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] GameObject parent;
    [SerializeField] Collider2D collider;
    [SerializeField] AudioSource pickUpAS;

    public int Collect()
    {
        parent.transform.DOScale(new Vector3(0.1f, 0.1f, 0f), 1f).OnComplete(() => Destroy(parent));
        collider.enabled = false;

        PlayCollectSound();

        return value;
    }

    private void PlayCollectSound()
    {
        pickUpAS.pitch = Random.Range(0.8f, 1.2f);
        pickUpAS.Play();
    }

}
