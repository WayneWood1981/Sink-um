using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{


    public List<Transform> targets = new List<Transform>();
    //public Transform[] targets;
    public Transform shipTarget;
    public Transform circlingTarget;

    public int shipSize;

    public Transform miniMap;

    public float movementSpeed;
    

    NavMeshAgent navMesh;
    Health health;

    public float speed = 1.0f;

    private Quaternion startRotation;


    public float rotationSpeed;
    public float orbitSpeed;

    private Quaternion lookRotation;
    private Vector3 direction;

    private Vector3 startingPos;

    
    private bool distanceToShipToBeginCircling;
    private float distanceToShip;
    private float distanceToTargeAroundShip;
    

    // Start is called before the first frame update
    void Start()
    {
        
        navMesh = GetComponent<NavMeshAgent>();
        startRotation = transform.rotation;
        health = GetComponent<Health>();

        PopulateTheListWithTargets();
        startingPos = this.transform.position;

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

    void PopulateTheListWithTargets()
    {
        Transform a = GameObject.Find("EnemyTarget1").transform;
        Transform b = GameObject.Find("EnemyTarget2").transform;
        Transform c = GameObject.Find("EnemyTarget3").transform;
        Transform d = GameObject.Find("EnemyTarget4").transform;
        Transform e = GameObject.Find("EnemyTarget5").transform;
        Transform f = GameObject.Find("EnemyTarget6").transform;
        Transform g = GameObject.Find("EnemyTarget7").transform;
        Transform h = GameObject.Find("EnemyTarget8").transform;

        

        targets.Add(a);
        targets.Add(b);
        targets.Add(c);
        targets.Add(d);
        targets.Add(e);
        targets.Add(f);
        targets.Add(g);
        targets.Add(h);

        miniMap.localPosition = new Vector3(0, 3200, 0);


        // Cant get this to work!!
        /*for (int i = 0; i < 8; i++)
        {
            GameObject target = GameObject.Find("EnemyTarget" + i);

            Debug.Log(target.name);
            targets.Add(target.transform);
        }*/
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
