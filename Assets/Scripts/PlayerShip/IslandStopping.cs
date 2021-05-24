using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IslandStopping : MonoBehaviour
{
    [SerializeField] ParticleSystem pS;

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

        digging = GetComponentInParent<DiggingUpTresure>();
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
                    coinParticles = true;
                    digging.treasureChest.transform.position += new Vector3(0, 0.001f, 0);
                    float distance = digging.treasureChest.transform.position.y - digging.treasureChestStartingPos.y;
                    
                    if (islandsStashOfGold <= 0)
                    {
                        digging.treasureChest.SetActive(false);
                        digging.openTreasureChest.SetActive(true);
                    }


                }
                else
                {
                    coinParticles = false;
                }

            }
            else
            {
                coinParticles = false;
            }

            

        }

        if (coinParticles)
        {
            if (!pS.isPlaying) pS.Play();
        }
        else
        {
            if (pS.isPlaying) pS.Stop();
        }
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
