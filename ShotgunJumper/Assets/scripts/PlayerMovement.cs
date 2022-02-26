using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInputActions _playerInputActions;
    private SpriteRenderer _gunSprite;
    private Vector3 _aimDirection;
    private LayerMask _mask = 1 << 6;
    private Gun _shotgun;
    private Gun _gunInHand;
    private float _angle;

    public GameObject bullet;
    public float groundRayLength;
    public float shootingForce;
    public float deceleration;
    public float acceleration;
    public float maxSpeed;
    public float jumpForce;
    public GameObject gunObject;

    private void Awake()
    {
        _shotgun = new Gun(10, 10, 10, 10);
        _gunInHand = _shotgun;
        
        _gunSprite = gunObject.GetComponentInChildren<SpriteRenderer>();
        _rb = this.GetComponent<Rigidbody2D>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        //_playerInputActions.Player.Jump.performed += _ => Jump();
        _playerInputActions.Player.Shoot.performed += _ => Shoot();
        
    }
    void Update()
    {   
        LookatMouse();
        //MovePlayer();
    }

    private void FixedUpdate()
    {
        var position = transform.position;
        var hit = Physics2D.Raycast(position, Vector2.down, groundRayLength, ~_mask);
        if (hit)
        {
            var velocity = _rb.velocity;
            if (math.abs(velocity.x) > 0.1)
            {
                velocity.x -= deceleration * velocity.normalized.x;
                _rb.velocity = velocity;
            }
        }
       
        Debug.DrawLine(position, position +Vector3.down * groundRayLength);
    }

    private void Shoot()
    {
        _rb.AddForce(-_aimDirection * shootingForce, ForceMode2D.Impulse);
        _shotgun.Shoot(bullet, gunObject.transform.position + gunObject.transform.right * 0.75f ,_angle);
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private void LookatMouse()
    {
        var gunTransform = gunObject.transform;
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
        _aimDirection = (positionInWorld - transform.position).normalized;
        _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        gunObject.transform.eulerAngles = new Vector3(0, 0, _angle);

    }

    private void MovePlayer()
    {
        if (Math.Abs(_rb.velocity.x) <= maxSpeed)
        {
            var movementValue = _playerInputActions.Player.HorizontalMovement.ReadValue<float>();
            var velocity = _rb.velocity.x;
            velocity += acceleration * Time.deltaTime * movementValue * 10;
            _rb.velocity = new Vector2(velocity, _rb.velocity.y);
        }
    }
}
