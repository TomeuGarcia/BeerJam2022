using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Transform referenceTransform;
    [SerializeField] Transform crosshairTransform;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] float duration = 0.5f;

    GameObject lastCollisionObject = null;
    Collider2D newCollision = null;
    Vector2 startPosition;
    Vector2 endPosition;
    float t;

    bool isColliding = false;


    private void Update()
    {
        if (!isColliding)
        {
            startPosition = crosshairTransform.position;

            t = Mathf.Clamp01(t -Time.deltaTime);
            crosshairTransform.position = Vector2.LerpUnclamped(startPosition, endPosition, t);
        }
        else
        {
            t = Mathf.Clamp01(t + Time.deltaTime);
            crosshairTransform.position = Vector2.LerpUnclamped(startPosition, endPosition, t);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("WeakSpot"))
        {
            startPosition = crosshairTransform.position;
            endPosition = collision.transform.position;
            isColliding = true;
            t = 0f;
            lastCollisionObject = collision.gameObject;

            crosshairTransform.DOPunchScale(new Vector3(0.2f, 0.2f, 0f), duration, 10, 1);
            //crosshairTransform.DOPunchPosition((collision.transform.position - crosshairTransform.position), duration, 5, 1, true);
            

            sr.DOColor(Color.red, duration / 2.0f).OnComplete(() => Restore());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (lastCollision = collision)
        //{
        //    crosshairTransform.position = collision.transform.position;
        //}
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == lastCollisionObject)
        {
            isColliding = false;
        }

        crosshairTransform.DOComplete();
        sr.DOComplete();

        lastCollisionObject = null;
    }

    private void Restore()
    {
        sr.DOColor(Color.white, duration / 2.0f);
    }


    //private void ToRedLooping()
    //{
    //    sr.DOColor(Color.red, duration / 2.0f).OnComplete(() => ToWhiteLooping());
    //}

    //private void ToWhiteLooping()
    //{
    //    sr.DOColor(Color.white, duration / 2.0f).OnComplete(() => ToRedLooping());
    //}


}
