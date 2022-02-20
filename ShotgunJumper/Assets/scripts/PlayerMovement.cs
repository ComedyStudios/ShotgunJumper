using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInputActions _playerInputActions;
    private SpriteRenderer _gunSprite; 
    public float acceleration;
    public float maxSpeed;
    public float jumpForce;
    public GameObject gun;

    private void Awake()
    {
        _gunSprite = gun.GetComponentInChildren<SpriteRenderer>();
        _rb = this.GetComponent<Rigidbody2D>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        _playerInputActions.Player.Jump.performed += _ => Jump();
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }
    
    void Update()
    {
        LookatMouse();

        //MovePlayer();
    }

    private void LookatMouse()
    {
        var gunTransform = gun.transform;
        if (gunTransform.eulerAngles.z > 90 && gunTransform.eulerAngles.z < 270 )
        {
            _gunSprite.flipY = true;
        }
        else
        {
            _gunSprite.flipY = false;
        }
        var positionOnScreen = _playerInputActions.Player.ScreenPosition.ReadValue<Vector2>();
        var positionInWorld = Camera.main.ScreenToWorldPoint(positionOnScreen);
        Debug.Log(positionOnScreen);
        var aimDirection = (positionInWorld - transform.position).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        gun.transform.eulerAngles = new Vector3(0, 0, angle);

    }

    private void MovePlayer()
    {
        if (Math.Abs(_rb.velocity.x) <= maxSpeed)
        {
            var MovementValue = _playerInputActions.Player.HorizontalMovement.ReadValue<float>();
            var velocity = _rb.velocity.x;
            velocity += acceleration * Time.deltaTime * MovementValue * 10;
            _rb.velocity = new Vector2(velocity, _rb.velocity.y);
        }
    }
}
