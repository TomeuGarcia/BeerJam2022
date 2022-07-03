using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Weakspot : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    Color startColor;
    [SerializeField] Color player0Color;
    [SerializeField] Color player1Color;

    const int maxDamage = 3;
    int damageCountByPlayer0 = 0;
    int damageCountByPlayer1 = 0;

    bool damagedByPlayer0 => damageCountByPlayer0 >= maxDamage;
    bool damagedByPlayer1 => damageCountByPlayer1 >= maxDamage;

    float colorLoopDuration = 2.0f;

    [SerializeField] float blobCooldownDuration = 1.0f;
    bool canBlob = false;


    public delegate void WeakspotAction();
    public static event WeakspotAction OnDeath;

    bool isAlive = true;

    [SerializeField] AudioSource takeDamageAS;
    [SerializeField] AudioSource noTakeDamageAS;



    private void Awake()
    {
        startColor = sr.GetComponent<SpriteRenderer>().color;

        StartCoroutine(BlobCooldown(Random.Range(0.0f, 2.0f)));
    }

    private void OnEnable()
    {
        CannonUse.OnPlayerDismount += Revive;
    }

    private void OnDisable()
    {
        CannonUse.OnPlayerDismount -= Revive;
    }


    private void Update()
    {
        if (canBlob)
        {
            transform.DOPunchScale(new Vector2(Random.Range(0.7f, 1f), Random.Range(0.7f, 1f)) * 0.1f, 0.5f);
            StartCoroutine(BlobCooldown(blobCooldownDuration));
        }
    }


    public void TakeDamage(int playerId)
    {
        if (playerId == 0)
        {
            if (damagedByPlayer0)
            {
                PlayNoTakeDamageSound();
            }
            else
            {
                ++damageCountByPlayer0;
                if (damagedByPlayer0) TurnPlayer1Color();
                PlayTakeDamageSound();
                PlayTakeDamageanimation();
            }
            
        }
        else if (playerId == 1)
        {
            if (damagedByPlayer1)
            {
                PlayNoTakeDamageSound();
            }
            else
            {
                ++damageCountByPlayer1;
                if (damagedByPlayer1) TurnPlayer0Color();
                PlayTakeDamageSound();
                PlayTakeDamageanimation();
            }
            
        }

        if (damagedByPlayer0 && damagedByPlayer1)
        {
            Die();
        }
    }


    private void TurnPlayer0Color()
    {
        sr.DOColor(player0Color, colorLoopDuration).OnComplete(() => sr.DOColor(startColor, colorLoopDuration/2f).OnComplete(() => TurnPlayer0Color()));
    }

    private void TurnPlayer1Color()
    {
        sr.DOColor(player1Color, colorLoopDuration).OnComplete(() => sr.DOColor(startColor, colorLoopDuration/2f).OnComplete(() => TurnPlayer1Color()));
    }


    private void Die()
    {
        isAlive = false;

        if (OnDeath != null) OnDeath();

        GetComponent<Collider2D>().enabled = false;
        transform.DOScale(Vector3.zero, 1f);

        sr.DOKill();
    }

    private void Revive()
    {
        if (!isAlive)
        {
            isAlive = true;
            GetComponent<Collider2D>().enabled = true;
            transform.DOScale(Vector3.one, 1f);
        }

        sr.DOKill();
        sr.color = startColor;

        damageCountByPlayer0 = damageCountByPlayer1 = 0;
    }


    public bool IsAlreadyDamaged(int cannonId)
    {
        if (cannonId == 0 && damagedByPlayer0) return true;
        if (cannonId == 1 && damagedByPlayer1) return true;

        return false;
    }


    IEnumerator BlobCooldown(float duration)
    {
        canBlob = false;
        yield return new WaitForSeconds(duration);
        canBlob = true;
    }



    private void PlayTakeDamageSound()
    {
        takeDamageAS.pitch = Random.Range(0.8f, 1.2f);
        takeDamageAS.Play();
    }

    private void PlayNoTakeDamageSound()
    {
        noTakeDamageAS.pitch = Random.Range(0.8f, 1.2f);
        noTakeDamageAS.Play();
    }

    private void PlayTakeDamageanimation()
    {
        sr.DOColor(Color.red, 0.2f).OnComplete(() => sr.DOColor(startColor, 0.2f));
    }


}
