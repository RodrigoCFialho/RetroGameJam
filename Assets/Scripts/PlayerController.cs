using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5f; // Movement speed
    public float dashSpeed = 50f; // Speed during dash
    public float dashDuration = 0.1f; // Duration of the dash
    public float dashCooldown = 0.5f; // Time before dash can be used again

    private PlayerInputActions playerInputActions; // Input actions
    private Rigidbody2D rb; // Rigidbody reference
    private Vector2 movementInput; // Stores input for movement
    private bool isDashing = false; // Is the player currently dashing?
    private float lastDashTime = -Mathf.Infinity; // Tracks the last time a dash was used

    void Awake() {
        // Initialize the Input Actions
        playerInputActions = new PlayerInputActions();
    }

    void OnEnable() {
        // Enable the Input Actions
        playerInputActions.Player.Enable();

        // Subscribe to the Move action
        playerInputActions.Player.Movement.performed += OnMove;
        playerInputActions.Player.Movement.canceled += OnMove;

        // Subscribe to the Dash action
        playerInputActions.Player.Dash.performed += OnDash;
    }

    void OnDisable() {
        // Disable the Input Actions
        playerInputActions.Player.Movement.performed -= OnMove;
        playerInputActions.Player.Movement.canceled -= OnMove;
        playerInputActions.Player.Dash.performed -= OnDash;

        playerInputActions.Player.Disable();
    }

    void Start() {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {

    }

    void FixedUpdate() {
        // If not dashing, move normally
        if(!isDashing) {
            rb.linearVelocity = movementInput * moveSpeed;
        }
    }

    private void OnMove(InputAction.CallbackContext context) {
        // Read movement input
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnDash(InputAction.CallbackContext context) {
        // Check cooldown
        if(Time.time < lastDashTime + dashCooldown)
            return;

        // Start the dash
        StartCoroutine(Dash());
    }

    private IEnumerator Dash() {
        isDashing = true;
        lastDashTime = Time.time;

        // Dash in the current movement direction
        rb.linearVelocity = movementInput.normalized * dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Stop dashing
        isDashing = false;
    }
}
