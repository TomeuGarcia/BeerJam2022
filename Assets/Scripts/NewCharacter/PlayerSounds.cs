using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [SerializeField] AudioSource playerWalking;
    [SerializeField] AudioSource playerJumping;
    [SerializeField] AudioSource playerLanding;
    [SerializeField] AudioSource playerAbility;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWalkingSound()
    {
        playerWalking.Play();
    }
    
    public void StopWalkingSound()
    {
        playerWalking.Stop();
    }

    public void PlayJumpingSound()
    {
        playerJumping.Play();
    }

    public void PlayLandingSound()
    {
        playerLanding.Play();
    }

    public void PlayAbilitySound()
    {
        playerAbility.Play();
    }
}
