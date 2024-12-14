using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D = null;

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float dashDuration = 0.1f;

    private float dashSpeed;
    private float dashCooldown = 2f;
    private int dashLength = 6; // Dash length based on the character's length
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    private void Awake()
    {
        float playerLength = GetComponent<SpriteRenderer>().bounds.size.y;

        myRigidbody2D = GetComponent<Rigidbody2D>();

        // Calculate speed based on length and duration
        dashSpeed = (dashLength * playerLength) / dashDuration;
    }

    public void EnableMovementEvent(Vector2 moveInput)
    {
        // movement
        myRigidbody2D.velocity = moveInput * speed;

        CheckFlip(moveInput.x);
    }

    public void EnableDashEvent() {
        // Check cooldown
        if(Time.time < lastDashTime + dashCooldown)
            return;

        Debug.Log("Dash action performed.");

        // Start the dash
        StartCoroutine(Dash());
    }

    private void CheckFlip(float horizontalInput)
    {
        if (horizontalInput < 0f && transform.right.x > 0f)
        {
            Flip();
        }
        else if (horizontalInput > 0f && transform.right.x < 0f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.right = -transform.right;
    }

    private IEnumerator Dash() {
        isDashing = true;
        lastDashTime = Time.time;

        // Dash in the current movement direction
        myRigidbody2D.velocity = myRigidbody2D.velocity.normalized * dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Stop dashing
        isDashing = false;
    }
}
