using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Door : IActivateable
{
    [SerializeField] Transform movingTransform;
    [SerializeField] Vector2 startPosition;
    [SerializeField] Vector2 endPosition;
    [SerializeField] float duration = 0.5f;
    [SerializeField] bool actionIsOpen = true;

    [SerializeField] AudioSource doorAS;


    public override void Activate()
    {
        if (actionIsOpen) OpenDoor();
        else CloseDoor();
    }

    private void OpenDoor()
    {
        doorAS.Play();
        
        movingTransform.DOMove(endPosition + (Vector2)transform.position, duration).OnComplete(
            () => doorAS.DOFade(0f, 0.25f).OnComplete( () =>doorAS.Stop()));
    }

    private void CloseDoor()
    {
        doorAS.Play();

        movingTransform.DOMove(startPosition + (Vector2)transform.position, duration).OnComplete(
            () => doorAS.DOFade(0f, 0.25f).OnComplete(() => doorAS.Stop()));
    }

}
