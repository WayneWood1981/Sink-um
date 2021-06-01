using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawnIn : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;

    [SerializeField] Canvas instructions;

    public AudioSource audioSource;

    public AudioClip squark;

    public GameObject instructCanvas;

    public Text instructionsText;

    private Transform playerSpawnPoint;

    public int timesSpawned;

    private void Awake()
    {
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        audioSource = GetComponent<AudioSource>();
        timesSpawned = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (timesSpawned == 1)
        {
            instructCanvas = Instantiate(instructions).gameObject;
            
            instructionsText = instructCanvas.GetComponentInChildren<Text>();

            if (instructionsText)
                audioSource.PlayOneShot(squark);
            instructionsText.text = "Ahoy! Captain\n\nThis is our Maiden Voyage in the hunt for the lost GOLD of\n\n'SeaLegs' Solomon!\n\n" +
                "I'm your trusty Parrot \"Yazzi\" and i'm here to help!.\n\nUse the mouse to click on the ocean.\nHead SOUTH - EAST from here to reach the next island!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNextPlayer()
    {
        GameObject newPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        timesSpawned += 1;

        FindObjectOfType<CameraFollow>().FindPlayer();
    }
    
}
