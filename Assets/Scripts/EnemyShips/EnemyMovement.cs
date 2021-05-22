using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

   
    public List<Transform> targets = new List<Transform>();
    public Transform shipTarget;
    public Transform circlingTarget;

    public int shipSize;
    
    

    public float movementSpeed;
    

    NavMeshAgent navMesh;
    Health health;

    public float speed = 1.0f;

    private Quaternion startRotation;


    public float rotationSpeed;
    public float orbitSpeed;

    private Quaternion lookRotation;
    private Vector3 direction;



    
    private bool distanceToShipToBeginCircling;
    private float distanceToShip;
    private float distanceToTargeAroundShip;
    

    // Start is called before the first frame update
    void Start()
    {
        
        navMesh = GetComponent<NavMeshAgent>();
        startRotation = transform.rotation;
        health = GetComponent<Health>();

    }

    // Update is called once per frame
    void Update()
    {
        
        

        if (shipTarget != null)
        {
            distanceToShip = Vector3.Distance(transform.position, shipTarget.transform.position);
        }
        else
        {
            float f = Mathf.PingPong(Time.time, 3) - 1.5f;
            transform.rotation = startRotation * Quaternion.AngleAxis(f, Vector3.forward);
        }



        if (distanceToShip <= 1)
        {
            distanceToShipToBeginCircling = true;

        }
        else
        {
            distanceToShipToBeginCircling = false;
        }

        
        if (!distanceToShipToBeginCircling && health.currentHealth > 0f)
        {
            getTargetDistances(targets);
            RotateShip();
            navMesh.SetDestination(shipTarget.transform.position);
            
            
        }
        else
        {
            

        }

        


    }

    

    private void getTargetDistances(List<Transform> possTargets)
    {
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < possTargets.Count; i++)
        {
            float distance = Vector3.Distance(targets[i].transform.position, transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                shipTarget = targets[i];
            }
        }
    }

    

    public void RotateShip()
    {

        if (shipTarget != null)
        {
            //find the vector pointing from my position to the target
            direction = (shipTarget.transform.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation(direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            //Add slight forward movement as to create an arc to arrive at destination.
            //Boat used to reverse otherwise.
            transform.localPosition += transform.forward * movementSpeed * Time.deltaTime;
        }

    }

    



    
}
