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

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1.0f / (gunData.fireRate / 60.0f) + gunData.bulletsPerShot * gunData.nextFireTime;

    public void Shoot()
    {

        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                Vector3 targetPoint;
                if(Physics.Raycast(transform.position + new Vector3(0, 1.00f, 0), transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IDamageable damageableObject = hitInfo.transform.GetComponent<IDamageable>();
                    targetPoint = hitInfo.point;
                    Debug.Log("Target hit");
                    damageableObject?.TakeDamage(gunData.damage);
                }
                else
                {
                    targetPoint = transform.position + new Vector3(0, 1.25f, 0);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;

                StartCoroutine(OnGunShoot(targetPoint, gunData.bulletsPerShot));
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

    IEnumerator OnGunShoot(Vector3 target, int bulletsPerShot)
    {
        for(int i = 0; i < bulletsPerShot; i++)
        {
            Vector3 directionWithoutSpread = -transform.forward;

            // Calculating random spread
            float x = Random.Range(-gunData.spread, gunData.spread);
            float y = Random.Range(-gunData.spread, gunData.spread);

            // Adding spread to the direction of the bullet
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            // Creating a copy of the bullet object and point it in the direction of the gun
            GameObject currentBullet = Instantiate(gunData.bullet, transform.position + new Vector3(0, 1.25f, 0), Quaternion.identity);
            currentBullet.transform.forward = directionWithSpread.normalized;

            // Adding force to the bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * gunData.shootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * gunData.upwardForce, ForceMode.Impulse);

            yield return new WaitForSeconds(gunData.nextFireTime);
        }
        
    }
}
