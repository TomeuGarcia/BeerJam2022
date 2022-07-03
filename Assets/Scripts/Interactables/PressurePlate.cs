using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PressurePlate : InteractableTrigger
{
    [SerializeField] Transform movingTransform;

    [SerializeField] AudioSource plateAS;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DelayedTrigger());
        }

    }

    public override void Trigger()
    {
        movingTransform.DOMoveY(0.3f + transform.position.y, 0.25f);

        base.Trigger();

        enabled = false;
    }


    IEnumerator DelayedTrigger()
    {
        plateAS.Play();

        yield return new WaitForSeconds(0.25f);

        Trigger();
    }



}
