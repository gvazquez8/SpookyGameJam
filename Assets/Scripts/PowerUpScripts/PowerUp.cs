using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect PowerUpEffect;
    public float timer;

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

    private void Update()
    {
        //set a timer of apx 30 seconds to destroy powerup
        if(timer > 0) 
            timer -= Time.deltaTime;

        else 
            Destroy(gameObject);
    }
}
