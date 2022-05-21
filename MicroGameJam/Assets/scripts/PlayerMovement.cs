using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public InputAction walkAction;

    // Start is called before the first frame update
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
        var movementVector = walkAction.ReadValue<Vector2>();
        Debug.Log(movementVector);
        this.transform.position += new Vector3(movementVector.x, movementVector.y, 0)*speed*Time.deltaTime;
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        
    }
}
