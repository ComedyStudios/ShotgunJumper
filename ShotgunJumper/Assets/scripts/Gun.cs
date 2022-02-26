using DefaultNamespace;
using UnityEngine;

public class Gun: ICanShoot
{
    private float _damage;
    private float _range;
    private float _reloadSpeed;
    private float _recoil;
    public Gun(float damage, float range, float reloadSpeed, float recoil)
    {
        _damage = damage;
        _range = range;
        _reloadSpeed = reloadSpeed;
        _recoil = recoil;
    }
    public void Shoot(GameObject bullet, Vector3 position, float angle)
    {
        var bulletClone = Object.Instantiate(bullet, position, Quaternion.Euler(0, 0 , angle));
        var bulletScript = bulletClone.GetComponent<BulletScript>();
        bulletScript.Fire(_recoil, _damage, _range);
    }
}