using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class TriggerScriptForDemo : MonoBehaviour
{
    public PlayerSpawnIn gameBrain;
    public Canvas instructionCanvas;
    public Health demoShipHealth;
    public AudioClip squawk;
    public AudioSource audioSource;

    private bool hasClickedM;
    private bool hasPlayedOnce;

    private bool waitingForDemoShipDeath;

    private void Start()
    {
        gameBrain = GameObject.Find("GameBrain").GetComponentInChildren<PlayerSpawnIn>();
        demoShipHealth = GameObject.Find("DemoShip").GetComponent<Health>();
        waitingForDemoShipDeath = true;
        hasClickedM = true;
    }

    private void Update()
    {
        if (demoShipHealth.isDead && waitingForDemoShipDeath)
        {
            if (!audioSource.isPlaying && !hasPlayedOnce)
            {
                audioSource.PlayOneShot(squawk, 0.5f);
                hasPlayedOnce = true;
            }

            hasClickedM = false;

            gameBrain.instructionsText.text = "YES!.. WELL DONE!\n\nRemember! There are no good ships among these GLISTENING waves Captain!\n\n" +
                "Depending on the strength of the ship depends on how much LOOT they drop. Sail over loot to pick it up.\n\n" +
                "When you're ready, set sail to the next destination on your MAP";
        }


        if(Input.GetKeyDown(KeyCode.M) && !hasClickedM)
        {
            gameBrain.instructCanvas.SetActive(false);
            gameBrain.timesSpawned += 1;
            hasClickedM = true;
        }

    }

    

    

    
}
