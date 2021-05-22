using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{

    Camera cam;

    

    public float movementSpeed;

    NavMeshAgent navMesh;

    public float speed = 1.0f;

    private Quaternion startRotation;


    public float RotationSpeed;

    private Quaternion lookRotation;
    private Vector3 direction;

    Vector3 target;

    private bool distanceReached;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        navMesh = GetComponent<NavMeshAgent>();
        startRotation = transform.rotation;

    }
    
    // Update is called once per frame
    void Update()
    {

        

        if (Input.GetMouseButton(0))
        {
            navMesh.speed = 0.75f;
            distanceReached = false;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit, 1000f);
            
            target = hit.point;

            
            navMesh.SetDestination(hit.point);

            
            
            
        }
      if(target != Vector3.zero)
        {
            distance = Vector3.Distance(transform.position, target);
        }
        else
        {
            float f = Mathf.PingPong(Time.time, 3) - 1.5f;
            transform.rotation = startRotation * Quaternion.AngleAxis(f, Vector3.forward);
        }
        
        if (distance <= 1)
        {
            navMesh.speed = 0.0f;
            distanceReached = true;
        }

        
        if (!distanceReached)
        {
            RotateShip();
        }

        


    }

    public void RotateShip()
    {

        if (target != Vector3.zero)
        {
            //find the vector pointing from my position to the target
            direction = (target - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation(direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);

            //Add slight forward movement as to create an arc to arrive at destination.
            //Boat used to reverse otherwise.
            transform.localPosition += transform.forward * movementSpeed * Time.deltaTime;
        }
        
    }

    
}
