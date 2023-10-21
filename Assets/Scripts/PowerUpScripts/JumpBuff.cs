using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/JumpBuff")]

public class JumpBuff : PowerUpEffect
{
    public override void Apply (GameObject target)
    {
        target.GetComponentInParent<PlayerMovement>().jumpPowerUp = true;
    }
}
