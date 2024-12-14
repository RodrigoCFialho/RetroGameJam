using UnityEngine;

public class SquareSpin : MonoBehaviour
{
    private float moveSpeed = 5f; // Movement speed of the player

    private Vector2 movement; // Stores the input direction
    private Rigidbody2D myRigidbody2D;   // Reference to the Rigidbody2D component

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get input for horizontal and vertical axes
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Combine into a Vector2 for movement
        movement = new Vector2(horizontal, vertical).normalized;
    }

    private void FixedUpdate() {
        // Move the player in the direction of the input
        myRigidbody2D.linearVelocity = movement * moveSpeed;
    }
}
