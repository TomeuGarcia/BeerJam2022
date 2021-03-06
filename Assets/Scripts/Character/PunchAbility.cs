using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputProcessor))]
public class PunchAbility : MonoBehaviour
{
    private PlayerInputProcessor playerInput;
    public float radius;
    public LayerMask punchableLayer;
    public CharacterAnimation anim;

    [SerializeField] PlayerSounds playerSounds;

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInputProcessor>();
        playerInput.OnAbilityInvoke += Punch;
    }
    private void OnDisable()
    {
        playerInput.OnAbilityInvoke -= Punch;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Canon"))
        {
            playerInput.OnAbilityInvoke -= Punch;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Canon"))
        {
            playerInput.OnAbilityInvoke += Punch;
        }
    }



    void Punch()
    {
        anim.Punch();
        playerSounds.PlayAbilitySound();
        Collider2D punchHit = Physics2D.OverlapCircle(transform.position, radius, punchableLayer);
        if (punchHit == null)
        {
            return;
        }
        Punchable punchable;
        if (punchHit.TryGetComponent(out punchable))
        {
            punchable.Punch();
        }
        
    }
}
