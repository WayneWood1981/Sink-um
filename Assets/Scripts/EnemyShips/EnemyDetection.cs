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

            enemyMovement.shipTarget = enemyMovement.getTargetDistances(enemyMovement.targets);
            enemyMovement.isPatrolling = false;
            //enemyMovement.shipTarget = other.transform;
        }
    }
}
