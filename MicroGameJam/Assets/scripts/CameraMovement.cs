using System;
using System.Collections;
using System.Collections.Generic;
using GameMechanics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    

    void Update()
    {
        var playerPosition = player.transform.position;
        var cameraPosition = transform.position;
        this.transform.Translate(new Vector3(playerPosition.x-cameraPosition.x,playerPosition.y-cameraPosition.y, 0) * speed);
    }
}
