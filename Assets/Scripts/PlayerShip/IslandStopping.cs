using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IslandStopping : MonoBehaviour
{

    

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {

            Debug.Log("OnIsland");
            other.transform.GetComponent<NavMeshAgent>().speed = 0f;
            other.transform.GetComponent<NavMeshAgent>().updateRotation = false;
            other.transform.GetComponent<NavMeshAgent>().isStopped = true;


        }
    }
}
