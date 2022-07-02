using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonAudio : MonoBehaviour
{
    [SerializeField] AudioSource canonMovingAS;
    [SerializeField] AudioSource shootingAS;
    [SerializeField] AudioSource ammoRechargeAS;



    public void PlayCanonMovingSound()
    {
        canonMovingAS.Play();
    }

    public void PauseCanonMovingSound()
    {
        canonMovingAS.Pause();
    }

    public void PlayShootSound()
    {
        shootingAS.pitch = Random.Range(0.8f, 1.2f);
        shootingAS.Play();
    }

    public void PlayChargeAmmoSound(float pitch)
    {
        if (ammoRechargeAS.isPlaying) return;
        ammoRechargeAS.pitch = pitch;
        ammoRechargeAS.Play();
    }
}
