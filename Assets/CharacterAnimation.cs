using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public CharacterMovement player;
    public Transform GraphicsRoot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Velocity", Mathf.Abs(player.playerInput.movementAxis.x));
        animator.SetBool("Falling", player.falling);
        animator.SetBool("Grounded", player.isGrounded);

        if (Mathf.Abs(player.playerInput.movementAxis.x) > 0.1f)
        GraphicsRoot.localScale = new Vector3(Mathf.Sign(player.playerInput.movementAxis.x),1,1);
        
    }

    private void FixedUpdate()
    {
        animator.SetBool("Grounded", player.isGrounded);
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void Punch()
    {
        animator.SetTrigger("Punch");
    }

    public void SetGrapple(bool grapp)
    {
        animator.SetBool("Grappling", grapp);
    }
}
