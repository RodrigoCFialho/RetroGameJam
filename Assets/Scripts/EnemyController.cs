using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private GameObject player;
    private Rigidbody2D rb;
    private Animator myAnimator;

    private Vector2 movementInput;
    private Vector2 lastMoveDirection;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) {
            Debug.LogError("Player not found! Ensure the Player GameObject has the 'Player' tag.");
        }
    }

    void Update() {
        if(player == null)
            return;

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

        rb.velocity = movementInput * speed;
    }

    private void Animations()
    {
        myAnimator.SetFloat("MoveY", movementInput.y);
        myAnimator.SetFloat("MoveX", movementInput.x);
        myAnimator.SetFloat("MoveMagnitude", movementInput.magnitude);
        myAnimator.SetFloat("LastMoveY", lastMoveDirection.y);
        myAnimator.SetFloat("LastMoveX", lastMoveDirection.x);
    }

    public void Die() {
        Destroy(gameObject);
    }
}