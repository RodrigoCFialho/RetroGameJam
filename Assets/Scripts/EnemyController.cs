using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;

    [SerializeField] private float speed = 1f;

    private Vector2 movementInput;
    private Vector2 lastMoveDirection;

    private bool isDead = false;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) 
        {
            Debug.LogError("Player not found! Ensure the Player GameObject has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player != null && !isDead)
        {
            //Store last move direction when we stop moving
            float moveX = (player.transform.position - transform.position).normalized.x;
            float moveY = (player.transform.position - transform.position).normalized.y;

            if ((moveX == 0 && moveY == 0) && (movementInput.x != 0 || movementInput.y != 0))
            {
                lastMoveDirection = movementInput;
            }

            // Calculate direction toward the player
            movementInput = (player.transform.position - transform.position).normalized;

            Animations();

            myRigidbody2D.velocity = movementInput * speed;
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

    public void WasHit() 
    {
        isDead = true;
        myAnimator.SetBool("IsDead", true);
        myRigidbody2D.velocity = Vector2.zero;
    }

    //Called by animation event
    public void Die()
    {
        Destroy(gameObject);
    }
}