using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private PlayerMovement PlayerMovementScript;
    private Rigidbody2D playerRb;
    private Rigidbody2D rb;
    public GameObject Player;
    public float cameraSpeed;
    
    private void Awake()
    {
        PlayerMovementScript = Player.GetComponent<PlayerMovement>();
        playerRb = Player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var playerVelocity = playerRb.velocity;
        rb.velocity = new Vector2(cameraSpeed * (Player.transform.position.x-transform.position.x), 0);
    }
}
