using UnityEngine;

public class SquareSpin : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player

    private Vector2 movement; // Stores the input direction
    public Rigidbody2D rb;   // Reference to the Rigidbody2D component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);

        // Get input for horizontal and vertical axes
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Combine into a Vector2 for movement
        movement = new Vector2(horizontal, vertical).normalized;
    }

    void FixedUpdate() {
        // Move the player in the direction of the input
        rb.linearVelocity = movement * moveSpeed;
    }
}
