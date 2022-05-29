using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float health;
    public float speed;
    public float minDistanceToPlayer;
    public float maxDistanceToPlayer;

    void Update() 
    {
        var direction = PlayerMovement.Player.transform.position - transform.position;
        if (direction.magnitude > minDistanceToPlayer && direction.magnitude < maxDistanceToPlayer)
        {
            transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        }
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
