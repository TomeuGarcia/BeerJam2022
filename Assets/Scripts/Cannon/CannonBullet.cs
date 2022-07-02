using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CannonBullet : MonoBehaviour
{
    int playerId;

    public void SetPlayerId(int playerId)
    {
        this.playerId = playerId;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("WeakSpot"))
        {
            Weakspot weakSpot = collision.gameObject.GetComponent<Weakspot>();
            AttackWeakspot(weakSpot);

            DoOnWeakspotContact();
        }
    }




    private void AttackWeakspot(Weakspot weakspot)
    {
        weakspot.TakeDamage(playerId);
    }


    void DoOnWeakspotContact()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = rb.velocity.normalized;
        rb.gravityScale = 0f;
        transform.DOPunchScale(new Vector3(-0.2f, -0.2f, 0f), 0.2f).OnComplete(() => Destroy(gameObject));
    }

}
