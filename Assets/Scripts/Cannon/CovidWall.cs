using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CovidWall : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] int weakspotAmount;
    int destroyedWeakspotAmount;

    [SerializeField] Transform wallTransform;

    private int numberOfPlayersOutOfAmmo = 0;


    public delegate void CovidWallAction();
    public static event CovidWallAction OnAllPlayersOutOfAmmo;
    public static event CovidWallAction OnDeathStart;
    public static event CovidWallAction OnDeathEnd;


    


    private void Update()
    {
        
    }


    private void OnEnable()
    {
        Weakspot.OnDeath += WeakspotWasDestoyed;
        CannonUse.OnPlayerDismount += () => destroyedWeakspotAmount = 0;
        CannonUse.OnPlayerRunsOutOfAmmo += DoPlayerOutOfAmmo;
    }

    private void OnDisable()
    {
        Weakspot.OnDeath -= WeakspotWasDestoyed;
        CannonUse.OnPlayerDismount -= () => destroyedWeakspotAmount = 0;
        CannonUse.OnPlayerRunsOutOfAmmo -= DoPlayerOutOfAmmo;
    }


    private void WeakspotWasDestoyed()
    {
        ++destroyedWeakspotAmount;

        if (destroyedWeakspotAmount >= weakspotAmount)
        {
            if (OnDeathStart != null) OnDeathStart();
            StartCoroutine(DestroyedAnimation());
        }

    }

    IEnumerator DestroyedAnimation()
    {
        yield return new WaitForSeconds(2f);
        wallTransform.DOPunchScale(new Vector3(0.4f, 0.2f, 0f), 2f).OnComplete(
            () => sr.DOFade(0f, 2f).OnComplete( 
                () => Die()));
    }

    private void DoPlayerOutOfAmmo()
    {
        if (++numberOfPlayersOutOfAmmo == 2)
        {
            if (OnAllPlayersOutOfAmmo != null) OnAllPlayersOutOfAmmo();
            numberOfPlayersOutOfAmmo = 0;
        }
    } 


    private void Die()
    {
        if (OnDeathEnd != null) OnDeathEnd();

        //gameObject.SetActive(false);
    }



}
