using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/DashBuff")]

public class DashBuff : PowerUpEffect 
{
    public override void Apply (GameObject target)
    {
        target.GetComponentInParent<Dashing>().dashCounter += 2;
        target.GetComponentInParent<buffSoundScript>().playSound = true;
    }
}

