using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect PowerUpEffect;

    //check for collision
    private void OnTriggerEnter(Collider collision)
    {
        // here we put the check if(player)
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            PowerUpEffect.Apply(collision.gameObject);
        }
        
    }
}
