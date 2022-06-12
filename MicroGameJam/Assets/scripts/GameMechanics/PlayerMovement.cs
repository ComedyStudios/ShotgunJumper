using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameMechanics
{
    public class PlayerMovement : MonoBehaviour
    {
        public static GameObject Player;
    
        public float speed;
        public InputAction walkAction;

        private Rigidbody2D _rb;

        private void Start()
        {
            Player = gameObject;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            walkAction.Enable();
        }

        private void OnDisable()
        {
            walkAction.Disable();
        }
        void Update()
        {
           
            
            //transform.Translate(  speed * Time.deltaTime * new Vector3(movementVector.x, movementVector.y, 0), Space.World);
            
        }

        private void FixedUpdate()
        {
            var dir = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            var movementVector = walkAction.ReadValue<Vector2>();

            _rb.velocity = movementVector * speed;
        }
    }
}
