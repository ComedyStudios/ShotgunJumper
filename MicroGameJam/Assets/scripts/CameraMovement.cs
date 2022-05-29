using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        var playerPosition = player.transform.position;
        var cameraPosition = transform.position;
        this.transform.Translate(new Vector3(playerPosition.x-cameraPosition.x,playerPosition.y-cameraPosition.y, 0) * speed);
    }
}
