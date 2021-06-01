using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : MonoBehaviour
{

    public Transform Player;

    public GameObject EnemyLVL1;
    public GameObject EnemyLVL2;
    public GameObject EnemyLVL3;
    public GameObject EnemyLVL4;
    public GameObject Boss;

    public GameObject floor;

    AudioSource audioSource;
    public AudioClip bossSong;


    public Transform[] spawnPoints;

    private CollectingResources playersResources;

    private float playersNotoriety;

    private bool releaseTheEnemies;
    private bool releaseTheEnemiesAgain;
    private bool releaseTheEnemiesAgainAgain;
    private bool releaseTheEnemiesAgainAgainAgain;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

        
        playersResources = FindObjectOfType<CollectingResources>();
        releaseTheEnemies = false;
        audioSource = GetComponent<AudioSource>();
    }

    

    // Update is called once per frame
    void Update()
    {
        

        

        if (playersResources.playersNotoriety >= playersResources.playersMaxNotoriety / 100 * 2 && playersResources.playersNotoriety <= playersResources.playersMaxNotoriety / 100 * 2.1)
        {
            if (releaseTheEnemies == false)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject newEnemy = Instantiate(EnemyLVL1, spawnPoints[i].position, Quaternion.identity);
                    
                    EnemyChase enemyChase = newEnemy.GetComponent<EnemyChase>();
                    enemyChase.isPatrolling = false;
                    Transform player = GameObject.FindGameObjectWithTag("Player").transform;
                    
                    enemyChase.shipTarget = player;
                    
                }
                
                releaseTheEnemies = true;
            }
        }

        if (playersResources.playersNotoriety >= playersResources.playersMaxNotoriety / 100 * 3 && playersResources.playersNotoriety <= playersResources.playersMaxNotoriety / 100 * 3.1)
        {
            if (releaseTheEnemiesAgain == false)
            {
                for (int i = 0; i < 3; i++)
                {
                    GameObject newEnemy = Instantiate(EnemyLVL2, spawnPoints[i].position, Quaternion.identity);

                    EnemyChase enemyChase = newEnemy.GetComponent<EnemyChase>();
                    enemyChase.isPatrolling = false;
                    Transform player = GameObject.FindGameObjectWithTag("Player").transform;

                    enemyChase.shipTarget = player;

                }
                releaseTheEnemiesAgain = true;
            }
        }

        if (playersResources.playersNotoriety >= playersResources.playersMaxNotoriety / 100 * 4)
        {
            if (releaseTheEnemiesAgainAgain == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    GameObject newEnemy = Instantiate(EnemyLVL3, spawnPoints[i].position, Quaternion.identity);

                    EnemyChase enemyChase = newEnemy.GetComponent<EnemyChase>();
                    enemyChase.isPatrolling = false;
                    Transform player = GameObject.FindGameObjectWithTag("Player").transform;

                    enemyChase.shipTarget = player;
                }
                releaseTheEnemiesAgainAgain = true;
            }
        }

        if (playersResources.playersNotoriety >= playersResources.playersMaxNotoriety / 100 * 5)
        {
            if (releaseTheEnemiesAgainAgainAgain == false)
            {
                for (int i = 0; i < 3; i++)
                {
                    GameObject newEnemy = Instantiate(EnemyLVL4, spawnPoints[i].position, Quaternion.identity);

                    EnemyChase enemyChase = newEnemy.GetComponent<EnemyChase>();
                    enemyChase.isPatrolling = false;
                    Transform player = GameObject.FindGameObjectWithTag("Player").transform;

                    enemyChase.shipTarget = player;
                }

                for (int i = 0; i < 1; i++)
                {
                    GameObject newEnemy = Instantiate(Boss, spawnPoints[i].position, spawnPoints[i].rotation);

                    EnemyChase enemyChase = newEnemy.GetComponent<EnemyChase>();
                    enemyChase.isPatrolling = false;
                    Transform player = GameObject.FindGameObjectWithTag("Player").transform;

                    enemyChase.shipTarget = player;

                    floor.GetComponent<MeshRenderer>().enabled = false;

                    audioSource.clip = bossSong;

                    audioSource.Play();
                }
                releaseTheEnemiesAgainAgainAgain = true;
            }
        }



    }
}
