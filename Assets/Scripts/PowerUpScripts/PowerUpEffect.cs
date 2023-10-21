using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "PowerUpEffect", menuName = "SpookyGameJam/PowerUpEffect", order = 0)]
public abstract class PowerUpEffect : ScriptableObject {
    public abstract void Apply(GameObject target);
    
}
