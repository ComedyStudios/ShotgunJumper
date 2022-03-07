using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInputActions _playerInputActions;
    private SpriteRenderer _gunSprite;
    private Vector2 _aimDirection;
    private Gun _shotgun;
    private Gun _gunInHand;
    private float _angle;
    private bool _canShoot;
    private float _shootingforce;

    private float movementSpeed;
    private float jumpForce;
    private float acceleration;

    public float hp;
    public float timeBetweenShots;
    public float effectiveJumpPadDistance;
    public float maxPlayerSpeedX;
    public float maxPlayerSpeedY;
    public float groundRayLength;
    public float shootingForceGround;
    public float shootingForceAir;
    public float deceleration;
    
    
    
    public GameObject gunObject;
    public GameObject bullet;

    private void Awake()
    {
        _shotgun = new Gun(10, 10, 10, 50);
        _gunInHand = _shotgun;
        
        _gunSprite = gunObject.GetComponentInChildren<SpriteRenderer>();
        _rb = this.GetComponent<Rigidbody2D>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        //_playerInputActions.Player.Jump.performed += _ => Jump();
        _playerInputActions.Player.Shoot.performed += _ => Shoot();

        _canShoot = true;
    }
    void Update()
    {   
        LookAtMouse();
        var velocity = _rb.velocity;
        if (Math.Abs(velocity.x) > maxPlayerSpeedX)
        {
            _rb.velocity = new Vector2(velocity.normalized.x * maxPlayerSpeedX,velocity.y);
        }

        if (Math.Abs(velocity.y)> maxPlayerSpeedY)
        {
            _rb.velocity = new Vector2(velocity.x, velocity.normalized.y * maxPlayerSpeedY);
        }
        //MovePlayer();
    }

    private void FixedUpdate()
    {
        var position = transform.position;
        var hit = Physics2D.Raycast(position, Vector2.down, groundRayLength, LayerMask.GetMask("Floor"));
        if (hit)
        {
            _shootingforce = shootingForceGround;
            var velocity = _rb.velocity;
            if (math.abs(velocity.x) > 0.1)
            {
                velocity.x -= deceleration * velocity.normalized.x;
                _rb.velocity = velocity;
            }
            
        }
        else
        {
            _shootingforce = shootingForceAir;
        }
       
        Debug.DrawLine(position, position +Vector3.down * groundRayLength);
        Debug.DrawLine(gunObject.transform.position, gunObject.transform.position + 10* new Vector3(_aimDirection.x, _aimDirection.y, 0),Color.red);
    }

    private void Shoot()
    {
        if (_canShoot)
        {
            var gunPosition = gunObject.transform.position;
            var hit = Physics2D.Raycast(gunPosition, _aimDirection, groundRayLength, ~LayerMask.GetMask("Player"));
           
            if (hit && hit.collider.gameObject.CompareTag("JumpPad"))
            {
                var jumpPad = hit.collider.gameObject;
                if (hit.distance < effectiveJumpPadDistance)
                {
                    var jumpPadForce = jumpPad.GetComponent<JumpPadScript>().jumpBoostFactor;
                    _shootingforce *= jumpPadForce;
                }
            }
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            Debug.Log((-_aimDirection.normalized * _shootingforce).magnitude);
            _rb.AddForce(-_aimDirection.normalized * _shootingforce, ForceMode2D.Impulse);
            _shotgun.Shoot(bullet, gunPosition + gunObject.transform.right * 0.75f ,_angle);
            StartCoroutine(StopShooting());
        }
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private void LookAtMouse()
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
        if (Camera.main != null)
        {
            var positionInWorld = Camera.main.ScreenToWorldPoint(positionOnScreen);
            _aimDirection = (positionInWorld - transform.position).normalized;
        }

        _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        gunObject.transform.eulerAngles = new Vector3(0, 0, _angle);

    }

    private void MovePlayer()
    {
        if (Math.Abs(_rb.velocity.x) <= movementSpeed)
        {
            var movementValue = _playerInputActions.Player.HorizontalMovement.ReadValue<float>();
            var velocity = _rb.velocity.x;
            velocity += acceleration * Time.deltaTime * movementValue * 10;
            _rb.velocity = new Vector2(velocity, _rb.velocity.y);
        }
    }

    IEnumerator StopShooting()
    {
        _canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        _canShoot = true;
    }
}
