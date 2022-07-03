using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputProcessor))]
public class CharacterMovement : MonoBehaviour
{
    public PlayerInputProcessor playerInput;
    public Rigidbody2D rb;
    public CharacterAnimation animation;
    public float maxSpeed;
    public float runAccel;
    public float runDecel;
    public int velPower;
    public float frictionAmount;
    
    public float force;
    public float jumpSpeed;
    float lastGroundedTime;
    public LayerMask groundLayer;
    public bool isGrounded;
    public Transform groundCheckPoint;
    bool jumping;
    public AnimationCurve accelerationCurve;
    public float MovementDir;
    public bool falling;
    private void OnEnable()
    {
        playerInput.OnJumpAction += OnJump;  
    }

    public void OnDisable()
    {
        playerInput.OnJumpAction -= OnJump;
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            animation.Jump();
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {   
        CheckCollision();
        DoForceMovement();
        falling = rb.velocity.y < 0;
    }


    private void DoForceMovement()
    {

        float targetSpeed = playerInput.movementAxis.x * maxSpeed;
        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccel : runDecel;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
        MovementDir = movement;
        if (lastGroundedTime > 0 && Mathf.Abs(playerInput.movementAxis.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    public void CheckCollision()
    {
        if (Physics2D.OverlapBox(groundCheckPoint.position, new Vector2(0.7f, 0.2f), 0, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    
    
}
