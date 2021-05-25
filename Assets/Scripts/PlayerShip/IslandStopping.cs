using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IslandStopping : MonoBehaviour
{
    [SerializeField] ParticleSystem pS;

    public AudioSource audioSource;
    public AudioClip[] digginSounds;
    public AudioClip chestOpen;
    public AudioClip goldCoins;
    private bool playDigging;
    private bool playChestOpen;

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
        islandsStashOfGold = Random.Range(500, 1000);
        
        playersCollection = FindObjectOfType<CollectingResources>();

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
                if(islandsStashOfGold > 0 && playersCollection.playersGold <= playersCollection.playersMaxGold)
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
                        }
                        
                        
                        pS.Play();

                        Invoke("StopPlayingPS", 5);
                        
                        digging.treasureChest.SetActive(false);
                        digging.openTreasureChest.SetActive(true);
                    }


                }
                

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
            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {

            isOnIsland = false;


        }
    }
}
