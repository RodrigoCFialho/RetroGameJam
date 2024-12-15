using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D myRigidbody2D = null;
    private Animator myAnimator = null;
    private HP_Manager hpManager = null;
    [SerializeField] private AudioSource playerAudioSource;


    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float dashDuration = 0.1f;

    [SerializeField]
    private int dashLength = 3; // Dash length based on the character's length


    [SerializeField] private AudioClip[] audioClips;

    [SerializeField]
    private float enemyDamage = 5f;


    // Dash stuff
    private float dashSpeed;
    [SerializeField] private float dashCooldown = 2f;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    private Vector2 movementInput = Vector2.zero;

    private Vector2 lastMoveDirection;

    private void Awake() {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        hpManager = GetComponent<HP_Manager>();

        // Calculate speed based on length and duration
        float playerLength = GetComponent<SpriteRenderer>().bounds.size.y;
        dashSpeed = (dashLength * playerLength) / dashDuration;
    }

    private void FixedUpdate() {
        if(!isDashing) {
            myRigidbody2D.velocity = movementInput * speed;

            Animations();
        }

        float timeScinceLastDash = Time.time - lastDashTime;

        if(timeScinceLastDash < dashCooldown) {
            GameManager.Instance.UpdateDashUI(timeScinceLastDash / dashCooldown);
        } else {
            GameManager.Instance.UpdateDashUI(1);
        }
    }

    private void Animations() {
        myAnimator.SetFloat("MoveY", movementInput.y);
        myAnimator.SetFloat("MoveX", movementInput.x);
        myAnimator.SetFloat("MoveMagnitude", movementInput.magnitude);
        myAnimator.SetFloat("LastMoveY", lastMoveDirection.y);
        myAnimator.SetFloat("LastMoveX", lastMoveDirection.x);
    }

    public void EnableMovementEvent(Vector2 moveInput) {
        //Store last move direction when we stop moving
        float moveX = moveInput.x;
        float moveY = moveInput.y;

        if((moveX == 0 && moveY == 0) && (movementInput.x != 0 || movementInput.y != 0)) {
            lastMoveDirection = movementInput;
        }

        // movement
        movementInput = moveInput;
    }

    public void EnableDashEvent() {
        // Check cooldown
        if((Time.time < lastDashTime + dashCooldown) || movementInput == Vector2.zero) {
            return;
        }


        // Start the dash
        StartCoroutine(Dash());
    }

    private IEnumerator Dash() {
        playerAudioSource.clip = audioClips[0];
        playerAudioSource.Play();
        isDashing = true;
        myAnimator.SetBool("IsDashing", true);
        lastDashTime = Time.time;

        hpManager.SetInvulnerability(true);

        // Dash in the current movement direction
        myRigidbody2D.velocity = myRigidbody2D.velocity.normalized * dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        hpManager.SetInvulnerability(false);

        // Stop dashing
        isDashing = false;
        myAnimator.SetBool("IsDashing", false);
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