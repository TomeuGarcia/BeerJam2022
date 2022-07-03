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
    [SerializeField] int bulletAmount = 0;
    int currentBulletAmount;

    public delegate void CanonAction();
    public static event CanonAction OnPlayerMount;
    public static event CanonAction OnPlayerDismount;
    public static event CanonAction OnPlayerRunsOutOfAmmo;


    [SerializeField] CanonAudio canonAudio;

    [SerializeField] GameObject controller;

    private void Awake()
    {
        crosshair.SetCannonId(canonId);

        controller.SetActive(false);
        //StartCoroutine(ShootLoop());
    }

    private void OnEnable()
    {
        OnPlayerMount += DoSetBulletAmount;
        CovidWall.OnAllPlayersOutOfAmmo += ActivateCanon;
    }

    private void OnDisable()
    {
        OnPlayerMount -= DoSetBulletAmount;
        CovidWall.OnAllPlayersOutOfAmmo -= ActivateCanon;
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

                    if (currentBulletAmount == 0)
                    {
                        if (OnPlayerRunsOutOfAmmo != null) OnPlayerRunsOutOfAmmo();
                    }
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

            bulletAmount = collision.gameObject.GetComponentInChildren<PickUpCollector>().pickUpCounter;

            characterMovementUser = collision.gameObject.GetComponent<CharacterMovement>();

            controller.SetActive(true);
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gamepadId = -1;
            isPlayerInside = false;

            characterMovementUser = null;

            controller.SetActive(false);
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

        canonAudio.PlayShootSound();
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

        canonAudio.PlayCanonMovingSound();
    }

    private void DeactivateCanon()
    {
        characterMovementUser.enabled = true;

        autoRotate.Pause();

        if (OnPlayerDismount != null) OnPlayerDismount();

        canonAudio.PauseCanonMovingSound();

        bulletAmountText.text = "";
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
        float soundPitch = 0.75f;
        for (int i = 1; i <= bulletAmount; ++i)
        {
            bulletAmountText.text = i.ToString();

            canonAudio.PlayChargeAmmoSound(soundPitch);
            soundPitch += Time.deltaTime * 4.0f;

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
