using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    GunData gunData;

    float timeSinceLastShot;

    public void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }
    
    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1.0f / (gunData.fireRate / 60.0f);

    public void Shoot()
    {

        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if(Physics.Raycast(transform.position + new Vector3(0, 1.25f, 0), transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IDamageable damageableObject = hitInfo.transform.GetComponent<IDamageable>();
                    damageableObject?.TakeDamage(gunData.damage);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShoot();
            }
        }
        else
        {
            StartReload();
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    private void OnGunShoot()
    {
        
    }
}
