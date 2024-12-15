using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private GameObject player;
    private Rigidbody2D rb;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) {
            Debug.LogError("Player not found! Ensure the Player GameObject has the 'Player' tag.");
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(player == null)
            return;

        // Calculate direction toward the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        rb.velocity = direction * speed;
    }
}
