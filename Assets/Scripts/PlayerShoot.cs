using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField]
    GameObject weaponSlot;
    [SerializeField]
    private KeyCode reloadKey;
    private void Update()
    {
        if (weaponSlot.GetComponent<Transform>()?.childCount != 0)
        {
            if (Input.GetMouseButton(0))
            {
                shootInput?.Invoke();
            }

            if (Input.GetKeyDown(reloadKey))
            {
                reloadInput?.Invoke();
            }
        }
    }
}
