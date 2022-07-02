using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using TMPro;


public class CannonUse : MonoBehaviour
{
    [SerializeField] int canonId = 0;

    [SerializeField] Crosshair crosshair;
    [SerializeField] AutoRotate autoRotate;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnTransform;
    [SerializeField] Transform bulletSpawnDirTransform;

    [SerializeField] Transform cannonMouthTransform;
    float cannonShotCooldown = 0.2f;

    CharacterMovement characterMovementUser;

    int gamepadId = -1;
    bool isInUse = false;
    bool canShoot = true;

    bool isPlayerInside = false;

    [SerializeField] Transform bulletAmountTransform;
    [SerializeField] TextMeshPro bulletAmountText;
    int bulletAmount = 20;
    int currentBulletAmount;

    public delegate void CanonAction();
    public static event CanonAction OnPlayerMount;
    public static event CanonAction OnPlayerDismount;




    private void Awake()
    {
        crosshair.SetCannonId(canonId);

        //StartCoroutine(ShootLoop());
    }

    private void OnEnable()
    {
        OnPlayerMount += DoSetBulletAmount;
    }

    private void OnDisable()
    {
        OnPlayerMount -= DoSetBulletAmount;
    }


    private void Start()
    {
        autoRotate.Pause();

    }

    private void Update()
    {
        if (isPlayerInside && gamepadId > -1)
        {
            bool use = Gamepad.all[gamepadId].buttonNorth.wasPressedThisFrame;
            if (use)
            {
                isInUse = !isInUse;
                if (isInUse)
                {
                    ActivateCanon();
                }
                else
                {
                    DeactivateCanon();
                }

            }

            if (isInUse && Gamepad.all[gamepadId].buttonSouth.wasPressedThisFrame && canShoot)
            {
                if (currentBulletAmount > 0)
                {
                    --currentBulletAmount;
                    Shoot();
                }
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gamepadId = collision.gameObject.GetComponent<PlayerInputProcessor>().gamepadId;
            isPlayerInside = true;

            characterMovementUser = collision.gameObject.GetComponent<CharacterMovement>();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gamepadId = -1;
            isPlayerInside = false;

            characterMovementUser = null;
        }
    }


    private void Shoot()
    {
        Vector2 shootDirection = (bulletSpawnDirTransform.position - bulletSpawnTransform.position).normalized;
        GameObject newAmmo = Instantiate(bullet, bulletSpawnTransform);
        newAmmo.GetComponent<Rigidbody2D>().AddForce(shootDirection * 50.0f, ForceMode2D.Impulse);
        newAmmo.GetComponent<CannonBullet>().SetPlayerId(canonId);

        cannonMouthTransform.DOComplete();
        cannonMouthTransform.DOPunchScale(new Vector3(-0.1f, 0.0f, 0.0f), cannonShotCooldown);

        SetTextMatchCurrentBulletAmount();
        bulletAmountTransform.DOPunchScale(new Vector3(0.2f, 0.2f, 0f), cannonShotCooldown);

        StartCoroutine(ShootPause());
    }

    IEnumerator ShootPause()
    {
        canShoot = false;

        crosshair.Pause();
        autoRotate.Pause();

        yield return new WaitForSeconds(cannonShotCooldown);

        crosshair.Resume();
        autoRotate.Resume();

        canShoot = true;
    }




    private void ActivateCanon()
    {
        characterMovementUser.rb.velocity = Vector2.zero;
        characterMovementUser.enabled = false;

        autoRotate.Resume();

        if (OnPlayerMount != null) OnPlayerMount();
    }

    private void DeactivateCanon()
    {
        characterMovementUser.enabled = true;

        autoRotate.Pause();

        if (OnPlayerDismount != null) OnPlayerDismount();
    }

    public void SetBulletAmount(int bulletAmount)
    {
        this.bulletAmount = bulletAmount;
    }

    private void RefreshCurrentBulletAmount()
    {
        currentBulletAmount = bulletAmount;
    }

    IEnumerator ProgressiveSetTextMatchCurrentBulletAmount()
    {
        for (int i = 1; i <= bulletAmount; ++i)
        {
            bulletAmountText.text = i.ToString();
            yield return new WaitForSeconds(Time.deltaTime * 3f);
        }        
    }

    private void DoSetBulletAmount()
    {
        RefreshCurrentBulletAmount();
        StartCoroutine(ProgressiveSetTextMatchCurrentBulletAmount());
    }


    private void SetTextMatchCurrentBulletAmount()
    {
        bulletAmountText.text = currentBulletAmount.ToString();
    }

}
