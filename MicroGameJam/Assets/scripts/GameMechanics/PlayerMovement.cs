using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static GameObject player;
    
    public float speed;
    public InputAction walkAction;

    private void Start()
    {
        player = this.gameObject;
    }

    private void OnEnable()
    {
        walkAction.Enable();
    }

    private void OnDisable()
    {
        walkAction.Disable();
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var dir = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        var movementVector = walkAction.ReadValue<Vector2>();
        this.transform.Translate(  speed * Time.deltaTime * new Vector3(movementVector.x, movementVector.y, 0), Space.World);
    }
    
}
