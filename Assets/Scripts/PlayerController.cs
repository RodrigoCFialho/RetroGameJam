using System;
using System.Collections;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D = null;
    private Collider2D myCollider2D = null;

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float dashDuration = 0.1f;

    [SerializeField]
    private int dashLength = 3; // Dash length based on the character's length

    [SerializeField]
    private float enemyDamage = 5f;

    // Dash stuff
    private float dashSpeed;
    private float dashCooldown = 2f;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    private Vector2 movementInput = Vector2.zero;

    private void Awake()
    {
        float playerLength = GetComponent<SpriteRenderer>().bounds.size.y;

        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<Collider2D>();

        // Calculate speed based on length and duration
        dashSpeed = (dashLength * playerLength) / dashDuration;
    }

    void FixedUpdate() {
        if(!isDashing) {
            myRigidbody2D.velocity = movementInput * speed;
        }
    }

    public void EnableMovementEvent(Vector2 moveInput)
    {
        // movement
        movementInput = moveInput;

        CheckFlip(moveInput.x);
    }

    public void EnableDashEvent() {
        // Check cooldown
        if(Time.time < lastDashTime + dashCooldown)
            return;

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

        myCollider2D.enabled = false;

        // Dash in the current movement direction
        myRigidbody2D.velocity = myRigidbody2D.velocity.normalized * dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        myCollider2D.enabled = true;

        // Stop dashing
        isDashing = false;
    }

    private void OnTriggerStay2D(UnityEngine.Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            HP_Manager healthManager = GetComponent<HP_Manager>();
            if(healthManager != null) {
                healthManager.TakeDamage(enemyDamage);
            }
        }
    }
}