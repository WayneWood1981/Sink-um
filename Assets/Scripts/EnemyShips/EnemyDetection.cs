using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private EnemyNav enemyMovement;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyNav>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {

            enemyMovement.shipTarget = enemyMovement.getTargetDistances(enemyMovement.targets);
            //enemyMovement.shipTarget = other.transform;
        }
    }
}
