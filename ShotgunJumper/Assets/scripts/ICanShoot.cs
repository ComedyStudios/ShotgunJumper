using UnityEngine;

namespace DefaultNamespace
{
    public interface ICanShoot
    {
        void Shoot(GameObject bullet, Vector3 position, float angle);
    }
}