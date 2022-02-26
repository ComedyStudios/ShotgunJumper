using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GunScript : MonoBehaviour
{
   public GameObject bulletPrefab;
   public void Fire(ICanShoot gun, Vector3 position, float angle)
   {
      gun.Shoot(bulletPrefab, position, angle);
   }
}
