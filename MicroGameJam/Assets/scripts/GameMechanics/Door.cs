using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameMechanics
{
    public class Door : MonoBehaviour
    {
        public bool canBeOpened;
        public InputAction doorOpenAction;

        private BoxCollider2D _collider;
        private SpriteRenderer _renderer;
        private void OnEnable()
        {
            doorOpenAction.Enable();
        }

        private void Start()
        {
            doorOpenAction.performed += _ => OpenDoor(); 
            _collider = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.color = Color.red;
        }

        private void OnDisable()
        {
            doorOpenAction.Disable();
        }

        private void OpenDoor()
        {
            if ((AttackScript.Instance.transform.position - transform.position).magnitude < GetComponent<CircleCollider2D>().radius)
            {
                if (EnemyManager.instance.enemies.Count == 0)
                {
                    _renderer.color = Color.green;
                    Debug.Log("Door is Opened");
                    _collider.enabled = false;
                }
                else Debug.Log("cant open door some enemies are hostile towards you ");
            }
        }
    }
}