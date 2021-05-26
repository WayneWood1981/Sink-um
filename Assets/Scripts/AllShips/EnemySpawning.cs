using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : MonoBehaviour
{

    [SerializeField] GameObject player;

    public Transform EnemyLVL1;
    public Transform EnemyLVL2;
    public Transform EnemyLVL3;
    public Transform EnemyLVL4;
    public Transform Boss;

    public Transform[] spawnPoints;

    private CollectingResources playersResources;

    private float playersNotoriety;

    private bool releaseTheEnemies;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

        
        playersResources = FindObjectOfType<CollectingResources>();


    }

    // Update is called once per frame
    void Update()
    {
        

        

        if (playersResources.playersNotoriety >= playersResources.playersMaxNotoriety / 100 * 25)
        {
            if (releaseTheEnemies == false)
            {
                for (int i = 0; i < 2; i++)
                {
                    Transform newEnemy = Instantiate(EnemyLVL1, spawnPoints[i].position, spawnPoints[i].rotation);
                    
                    newEnemy.GetComponent<EnemyChase>().shipTarget = player.transform;
                }
                releaseTheEnemies = true;
            }
        }else if (playersResources.playersNotoriety >= playersResources.playersMaxNotoriety / 100 * 50)
        {
            if (releaseTheEnemies == false)
            {
                for (int i = 0; i < 3; i++)
                {
                    Transform newEnemy = Instantiate(EnemyLVL2, spawnPoints[i].position, spawnPoints[i].rotation);
                    newEnemy.GetComponent<EnemyChase>().shipTarget = player.transform;

                }
                releaseTheEnemies = true;
            }
        }else if (playersResources.playersNotoriety >= playersResources.playersMaxNotoriety / 100 * 75)
        {
            if (releaseTheEnemies == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    Transform newEnemy = Instantiate(EnemyLVL3, spawnPoints[i].position, spawnPoints[i].rotation);
                    newEnemy.GetComponent<EnemyChase>().shipTarget = player.transform;
                }
                releaseTheEnemies = true;
            }
        }
        else if (playersResources.playersNotoriety >= playersResources.playersMaxNotoriety)
        {
            if (releaseTheEnemies == false)
            {
                for (int i = 0; i < 3; i++)
                {
                    Transform newEnemy = Instantiate(EnemyLVL4, spawnPoints[i].position, spawnPoints[i].rotation);
                    newEnemy.GetComponent<EnemyChase>().shipTarget = player.transform;
                }

                for (int i = 0; i < 1; i++)
                {
                    Transform newEnemy = Instantiate(Boss, spawnPoints[i].position, spawnPoints[i].rotation);
                    newEnemy.GetComponent<EnemyChase>().shipTarget = player.transform;
                }
                releaseTheEnemies = true;
            }
        }



    }
}
