using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buffSoundScript : MonoBehaviour
{
    [Header("References")]
    //public PlayerMovement pm;
    public bool playSound;
    public GameObject powerUpSound;

    void Start()
    {
        playSound = false;
        powerUpSound.SetActive(false);
    }
    
    void Update()
    {
        if(playSound == true)
        {
            StartCoroutine(play());
        }
    }

    private IEnumerator play()
    {
        powerUpSound.SetActive(true);
        yield return new WaitForSeconds(1);

        powerUpSound.SetActive(false);
        playSound = false;
    }
}