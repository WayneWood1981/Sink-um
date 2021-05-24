using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTest : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    Camera cam;
    Vector3 target;
    NavMeshPath path;
    public Transform ball;

    bool distanceReached;
    private void Start()
    {
        cam = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {

            navMeshAgent.speed = 0.75f;
            distanceReached = false;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit, 1000f);

            target = hit.point;


            navMeshAgent.SetDestination(hit.point);

            
            Instantiate(ball, target, Quaternion.identity);
        }

    }
}
    

