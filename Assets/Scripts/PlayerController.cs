using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private Health healthScript;

    //audio
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip[] audioClips;

    //move speed
    [SerializeField] private float speed = 3f;

    private Vector2 movementInput = Vector2.zero;
    private Vector2 lastMoveDirection;

    // Dash stuff
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private int dashLength = 3; // Dash length based on the character's length
    [SerializeField] private float dashCooldown = 2f;
    private bool isDashing = false;
    private float dashSpeed;
    private float playerLength;
    private float lastDashTime = -Mathf.Infinity;

    [SerializeField] private float enemyDamage = 5f;

    private void Awake() 
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        healthScript = GetComponent<Health>();

        playerLength = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Start()
    {
        // Calculate speed based on length and duration
        dashSpeed = (dashLength * playerLength) / dashDuration;
    }

    private void FixedUpdate() 
    {
        if (!isDashing) 
        {
            myRigidbody2D.velocity = movementInput * speed;

            Animations();
        }

        float timeSinceLastDash = Time.time - lastDashTime;

        if (timeSinceLastDash < dashCooldown) 
        {
            GameManager.Instance.UpdateDashUI(timeSinceLastDash / dashCooldown);
        } 
        else 
        {
            GameManager.Instance.UpdateDashUI(1);
        }
    }

    private void Animations() 
    {
        myAnimator.SetFloat("MoveY", movementInput.y);
        myAnimator.SetFloat("MoveX", movementInput.x);
        myAnimator.SetFloat("MoveMagnitude", movementInput.magnitude);
        myAnimator.SetFloat("LastMoveY", lastMoveDirection.y);
        myAnimator.SetFloat("LastMoveX", lastMoveDirection.x);
    }

    public void EnableMovementEvent(Vector2 moveInput) 
    {
        //Store last move direction when we stop moving
        float moveX = moveInput.x;
        float moveY = moveInput.y;

        if ((moveX == 0 && moveY == 0) && (movementInput.x != 0 || movementInput.y != 0)) 
        {
            lastMoveDirection = movementInput;
        }

        // movement
        movementInput = moveInput;
    }

    public void EnableDashEvent() 
    {
        // Check cooldown
        if ((Time.time < lastDashTime + dashCooldown) || movementInput == Vector2.zero) 
        {
            return;
        }

        // Start the dash
        StartCoroutine(Dash());
    }

    private IEnumerator Dash() 
    {
        isDashing = true;
        myAnimator.SetBool("IsDashing", true);

        lastDashTime = Time.time;

        healthScript.SetInvulnerability(true);

        //Play audio
        playerAudioSource.clip = audioClips[0];
        playerAudioSource.Play();

        // Dash in the current movement direction
        myRigidbody2D.velocity = myRigidbody2D.velocity.normalized * dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        healthScript.SetInvulnerability(false);

        // Stop dashing
        isDashing = false;
        myAnimator.SetBool("IsDashing", false);
    }

    private void OnTriggerStay2D(UnityEngine.Collider2D collision) 
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
        {
            healthScript.TakeDamage(enemyDamage);
        }
    }
}