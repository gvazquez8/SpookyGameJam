using UnityEngine;


[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;
    public float spread;
    public bool allowButtonHold;
    public int bulletsPerShot;
    public float nextFireTime;

    [Header("Projectiles")]
    public GameObject bullet;
    public float shootForce;
    public float upwardForce;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    [HideInInspector]
    public bool reloading;
}
