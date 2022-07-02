using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Transform referenceTransform;
    [SerializeField] Transform crosshairTransform;
    [SerializeField] SpriteRenderer crosshairSr;
    [SerializeField] float duration = 0.5f;

    [SerializeField] GameObject weakSpotMarker;

    GameObject lastCollisionObject;

    int cannonId; 


    private void Awake()
    {
        weakSpotMarker.SetActive(false);
    }

    private void OnEnable()
    {
        CannonUse.OnPlayerDismount += () => weakSpotMarker.SetActive(false);
    }

    private void OnDisable()
    {
        CannonUse.OnPlayerDismount -= () => weakSpotMarker.SetActive(false);
    }


    public void SetCannonId(int cannonId)
    {
        this.cannonId = cannonId;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("WeakSpot"))
        {
            if (collision.gameObject.GetComponent<Weakspot>().IsAlreadyDamaged(cannonId)) return;

            lastCollisionObject = collision.gameObject;

            crosshairTransform.DOPunchScale(new Vector3(0.2f, 0.2f, 0f), duration, 10, 1);
            crosshairSr.DOColor(Color.red, duration / 2.0f).OnComplete(() => Restore());

            weakSpotMarker.SetActive(true);
            weakSpotMarker.transform.position = collision.gameObject.transform.position;
            //weakSpotMarkerSr.DOColor(Color.red, duration / 2.0f).OnComplete(() => weakSpotMarkerSr.DOColor(Color.white, duration / 2.0f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == lastCollisionObject)
        {
            crosshairTransform.DOComplete();
            crosshairSr.DOComplete();

            weakSpotMarker.SetActive(false);
        }
        
    }

    private void Restore()
    {
        crosshairSr.DOColor(Color.white, duration / 2.0f);
    }


    private void ToRedLooping(SpriteRenderer sr)
    {
        sr.DOColor(Color.red, duration / 2.0f).OnComplete(() => ToWhiteLooping(sr));
    }

    private void ToWhiteLooping(SpriteRenderer sr)
    {
        sr.DOColor(Color.white, duration / 2.0f).OnComplete(() => ToRedLooping(sr));
    }



    public void Pause()
    {
        crosshairTransform.DOPause();
        crosshairSr.DOPause();
    }

    public void Resume()
    {
        crosshairTransform.DOPlay();
        crosshairSr.DOPlay();
    }

}
