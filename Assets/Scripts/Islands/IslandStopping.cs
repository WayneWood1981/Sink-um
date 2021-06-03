using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IslandStopping : MonoBehaviour
{
    [SerializeField] ParticleSystem pS;
    PlayerSpawnIn gameBrain;

    

    public AudioSource audioSource;
    public AudioClip[] digginSounds;
    public AudioClip chestOpen;
    public AudioClip goldCoins;
    public AudioClip squark;

    private bool playDigging;
    private bool playChestOpen;
    private bool displayWarning;
    private bool displayHoldSpace;

    bool displayDigText;

    public CollectingResources playersCollection;
    
    public DiggingUpTresure digging;


    private bool coinParticles;
    public bool isOnIsland;

    public float islandsStashOfGold;

    public float speedOfLooting;

    public float digSpeed = 0.01f;

    public float reachDigSpeed;

    Time time;

    private void Start()
    {
        islandsStashOfGold = 1000f;
        
        playersCollection = FindObjectOfType<CollectingResources>();

        gameBrain = GameObject.Find("GameBrain").GetComponentInChildren<PlayerSpawnIn>();

        pS = GetComponentInChildren<ParticleSystem>();
        pS.Stop();

        digging = GetComponentInParent<DiggingUpTresure>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isOnIsland)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                playersCollection = FindObjectOfType<CollectingResources>();
                if (islandsStashOfGold > 0 && playersCollection.playersGold <= playersCollection.playersMaxGold)
                {
                    islandsStashOfGold -= 1 * speedOfLooting * Time.deltaTime;
                    playersCollection.playersGold += 1 * speedOfLooting * Time.deltaTime;
                    
                    
                    digging.treasureChest.transform.position += new Vector3(0, 0.001f, 0);
                    float distance = digging.treasureChest.transform.position.y - digging.treasureChestStartingPos.y;

                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(digginSounds[Random.Range(0, digginSounds.Length)], 0.3f);
                    }
                    
                    if (islandsStashOfGold <= 0)
                    {
                        if (!playChestOpen)
                        {
                            audioSource.PlayOneShot(chestOpen, 0.5f);
                            audioSource.PlayOneShot(goldCoins, 0.25f);
                            
                            playChestOpen = true;
                            playersCollection.playerscannonBalls += 10;
                            FindObjectOfType<PlayersHealth>().currentHealth += 25f;
                        }
                        
                        
                        pS.Play();

                        Invoke("StopPlayingPS", 5);

                        digging.treasureChest.GetComponent<MeshRenderer>().enabled = false;
                        digging.openTreasureChest.GetComponent<MeshRenderer>().enabled = true;
                        digging.openLid.GetComponent<MeshRenderer>().enabled = true;
                        displayWarning = true;

                    }
                    if (gameBrain.timesSpawned < 2)
                    {
                        if (!displayHoldSpace)
                        {
                            audioSource.PlayOneShot(squark, 0.5f);
                            displayHoldSpace = true;
                        }
                        
                        
                        
                        
                        gameBrain.instructionsText.text = "HOLD SPACE\n\nNow we will start to steal ol' Sealeg's Gold\n\n" +
                            "Legend states that he will return from the dead to hunt down the Pirate that stole his gold!\n\n" +
                            "All of 'SeaLegs' chests are located on the Map, we are the BLUE Marker.";
                    }
                        

                }
                

            }




        }
        
        if (displayWarning)
        {
            if (gameBrain.timesSpawned < 2)
            {

                
                audioSource.PlayOneShot(squark, 0.5f);
                

                gameBrain.instructionsText.text = "WARNING!\n\nThe more of his gold on your ship the more of his" +
                                " undead fleet he will send after you to claim it back\n\nThe skull is your Reputation. " +
                                "The higher it is the more ships will be after our stolen gold\n\n" +
                                "Sail EAST, I see an Enemy ship!";
                displayWarning = false;
            }
        }
                
        
    }

    void StopPlayingPS()
    {
        pS.Stop();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            
            isOnIsland = true;
            if (gameBrain.timesSpawned < 2)
            {
                gameBrain.instructCanvas.SetActive(true);
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(squark, 0.5f);
                }
                
                gameBrain.instructionsText.text = "LAND AHOY!\n\n\nGreat we're here...\nEach island has a JETI, find this islands JETI and stop next to it.\n\n\n" +
                    "Once next to the Jeti, hold SPACE.";
            }
            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            displayWarning = false;
            isOnIsland = false;
            if (gameBrain.timesSpawned < 2)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(squark, 0.5f);
                }
                gameBrain.instructionsText.text = "STOP!!... they're just up ahead!\n\n" +
                    "Use your CROWS NEST \'Z\' to take a better look.\nRemember to press it again when in combat.\n\n" +
                    "Use \'A\' & \'D\' to fire from each side of the ship.\n\n" +
                    "If they sink us, we will have to grab another boat an sail back for our GOLD!";
            }

        }
    }
}
