using System.Collections.Generic;
using MiniGameJam;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utils;

namespace GameMechanics
{
    public class AttackScript : MonoBehaviour
    {
    
        public InputAction attackAction;
        public Weapon currentWeapon;
        public static AttackScript Instance;
        public float lastHit;

        private readonly List<Ray2D> _rays = new List<Ray2D>();
        private float _spread;
        private int _rayCount;
        private float _rayLength;
        private float _hitCooldown;
        private int _damage;

        public void OnEnable()
        {
            attackAction.Enable();
            _spread = currentWeapon.spread;
            _rayCount = currentWeapon.rayCount;
            _rayLength = currentWeapon.rayLength;
            _hitCooldown = currentWeapon.hitCooldown;
            _damage = currentWeapon.damage;
        }

        public void OnDisable()
        {
            attackAction.Disable();
        }

        private void Start()
        {
            Instance = this;
            attackAction.performed += _ => DoDamage();
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
            //TODO: do not hit when changing weapons 

            
            if (Time.time-lastHit >= currentWeapon.hitCooldown && !CanvasUtils.IsPointerOverUIElement())
            {
                currentWeapon.UseWeapon();
                lastHit = Time.time;
            }
        }
        
        
    
    }
}
