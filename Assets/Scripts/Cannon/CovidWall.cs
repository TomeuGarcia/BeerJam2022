using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CovidWall : MonoBehaviour
{
    [SerializeField] int weakspotAmount;
    int destroyedWeakspotAmount;

    [SerializeField] Transform wallTransform;


    private void OnEnable()
    {
        Weakspot.OnDeath += WeakspotWasDestoyed;
        CannonUse.OnPlayerDismount += () => destroyedWeakspotAmount = 0;
    }

    private void OnDisable()
    {
        Weakspot.OnDeath -= WeakspotWasDestoyed;
        CannonUse.OnPlayerDismount -= () => destroyedWeakspotAmount = 0;

    }


    private void WeakspotWasDestoyed()
    {
        ++destroyedWeakspotAmount;

        if (destroyedWeakspotAmount >= weakspotAmount)
        {


            StartCoroutine(DestroyedAnimation());
        }

    }

    IEnumerator DestroyedAnimation()
    {
        yield return new WaitForSeconds(2f);
        wallTransform.DOPunchScale(new Vector3(0.4f, 0.2f, 0f), 2f).OnComplete(() => gameObject.SetActive(false));
    }

}
