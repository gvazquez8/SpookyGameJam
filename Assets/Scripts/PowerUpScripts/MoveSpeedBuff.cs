using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/MoveSpeedBuff")]

public class MoveSpeedBuff : PowerUpEffect
{
    public override void Apply(GameObject target)
    {   
        target.GetComponentInParent<PlayerMovement>().speedPowerUp = true;
    }
    
}
