using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;


public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 60f;
    [SerializeField] float acceleration = 40f;
    float speed = 0.0f;
    [SerializeField] float force;
    [SerializeField] float jumpSpeed;

    Vector2 velocity;

    bool jumpDesired;

    Vector2 playerInput;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput = Vector2.ClampMagnitude(playerInput, 1.0f);

        jumpDesired |= Input.GetButtonDown("Jump");
        
    }


    private void FixedUpdate()
    {
        //rb.AddForce(playerInput * force);

        //Vector2 v = rb.velocity;
        //v.x = Mathf.Clamp(v.x, -force, force);
        //rb.velocity = v;

        UpdateState();
        AdjustVelocity();

        if (jumpDesired) {
            jumpDesired = false;
            Jump();
        }

        rb.velocity = velocity;

    }


    private void UpdateState()
    {
        velocity = rb.velocity;
    }


    private void AdjustVelocity()
    {
        float acceleration = this.acceleration;
        float speed = maxSpeed;

        float currentX = velocity.x;
        float currentY = velocity.y;

        float maxSpeedChange = acceleration * Time.fixedDeltaTime;

        float newX = Mathf.MoveTowards(currentX, playerInput.x * speed, maxSpeedChange);

        velocity += Vector2.right * (newX - currentX);


    }



    private void Jump()
    {
        velocity += Vector2.up * jumpSpeed;
    }


}
