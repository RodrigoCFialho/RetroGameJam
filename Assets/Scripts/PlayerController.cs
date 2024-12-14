using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D = null;

    [SerializeField]
    private float speed = 3f;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void EnableMovementEvent(Vector2 moveInput)
    {
        // movement
        myRigidbody2D.velocity = moveInput * speed;

        CheckFlip(moveInput.x);
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
}
