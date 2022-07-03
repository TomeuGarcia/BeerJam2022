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

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInputProcessor>();
        playerInput.OnAbilityInvoke += Punch;
    }
    private void OnDisable()
    {
        playerInput.OnAbilityInvoke -= Punch;
    }

    void Punch()
    {
        anim.Punch();
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
