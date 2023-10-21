using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private const float MAX_HEALTH = 100f;
    private const float MAX_SHIELD = 50f;
    public float health;
    public float shield;

    [SerializeField]
    private GameObject healthBar;
    private Image healthBarImage;

    [SerializeField]
    private GameObject shieldBar;
    private Image shieldBarImage;

    // Start is called before the first frame update
    void Start()
    {
        health = MAX_HEALTH;
        shield = MAX_SHIELD;
        healthBarImage = healthBar.GetComponent<Image>();
        shieldBarImage = shieldBar.GetComponent<Image>();
    }

    private void Update()
    {
        healthBarImage.fillAmount = health / MAX_HEALTH;
        shieldBarImage.fillAmount = shield / MAX_SHIELD;
    }


}
