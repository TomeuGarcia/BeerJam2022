using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;



public class AmmoPickUpDisplay : MonoBehaviour
{
    [SerializeField] Transform movingTransform;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] TextMeshPro text;


    public void StartAnimation(int ammoAmount, Vector3 startPosition)
    {
        transform.position = startPosition;

        text.text = "+" + ammoAmount;

        movingTransform.DOMoveY(transform.position.y + 2f, 3f);

        StartCoroutine(DelayedFade());
    }

    IEnumerator DelayedFade()
    {
        yield return new WaitForSeconds(2f);
        sr.DOFade(0f, 2f).OnComplete(() => Destroy(gameObject));
    }


}
