using System.Collections.Generic;
using MiniGameJam;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameMechanics
{
    public class AttackScript : MonoBehaviour
    {
    
        public InputAction attackAction;
        public Weapon currentWeapon;
        public static AttackScript Instance;
        private readonly List<Ray2D> _rays = new List<Ray2D>();
        private float _spread;
        private int _rayCount;
        private float _rayLength;
        private float _hitCooldown;
        private int _damage;
        private float _lastHit;

        public void OnEnable()
        {
            attackAction.Enable();
        }

        public void OnDisable()
        {
            attackAction.Disable();
        }

        private void Start()
        {
            Instance = this;
            SetWeapon(currentWeapon);
            attackAction.performed += _ => DoDamage();
        }

        public void SetWeapon(Weapon weapon)
        {
            _spread = weapon.spread;
            _rayCount = weapon.rayCount;
            _rayLength = weapon.rayLength;
            _hitCooldown = weapon.hitCooldown;
            _damage = weapon.damage;
            currentWeapon = weapon;
        }

        public void Update()
        {
            _rays.Clear();
            for (int i = 0; i< _rayCount; i++)
            {
                _rays.Add(new Ray2D(transform.position,  Quaternion.Euler(0, 0, _spread * (i-_rayCount/2)) *transform.right));
            }
            foreach (var ray in _rays)
            {
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _rayLength,~LayerMask.GetMask("Player", "Item"));
                if (hit == false)
                {
                    Debug.DrawLine(ray.origin,ray.origin + ray.direction*_rayLength, Color.red);
                }
                else
                {
                    Debug.DrawLine(ray.origin,ray.origin + ray.direction*hit.distance, Color.red);
                }
            }
        }

        private void DoDamage()
        {
            //TODO: works for now maybe Expand later
            if (Time.time-_lastHit >= _hitCooldown)
            {
                _lastHit = Time.time;
                List<GameObject> targetsInRange = new List<GameObject>();
                foreach (var ray in _rays)
                {
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _rayLength,~LayerMask.GetMask("Player", "Item"));
                    if (hit == true && !targetsInRange.Contains(hit.collider.gameObject))
                    {
                        targetsInRange.Add(hit.collider.gameObject);
                    }
                }
                foreach (var target in targetsInRange)
                {
                    var state = target.GetComponent<EnemyState>();
                    state.currentHealth -= _damage;
                    Debug.Log($"{target.name} has been hit, it has {state.currentHealth} health left");
                }
            }
        }
    
    }
}
