using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private EnemyChase enemyMovement;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyChase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            
            enemyMovement.targets.Clear();
            enemyMovement.PopulateTheListWithTargets();
            enemyMovement.shipTarget = enemyMovement.getTargetDistances(enemyMovement.targets);
            enemyMovement.isPatrolling = false;
            //enemyMovement.shipTarget = other.transform;
        }
    }

    public void TrackPlayer()
    {
        enemyMovement.targets.Clear();
        enemyMovement.PopulateTheListWithTargets();
        enemyMovement.shipTarget = enemyMovement.getTargetDistances(enemyMovement.targets);
        enemyMovement.isPatrolling = false;
    }
}
