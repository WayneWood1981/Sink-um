using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{

    Camera cam;

    [SerializeField] GameObject canvasGameObject;

    public float movementSpeed;

    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    
    public float speed = 1.0f;

    private Quaternion startRotation;

    public Transform miniMapTransform;

    public float RotationSpeed;

    private Quaternion lookRotation;
    private Vector3 direction;
    private bool canvasToggled;

    Vector3 target;

    private bool distanceReached;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        startRotation = transform.rotation;

    }


    
    
    // Update is called once per frame
    void Update()
    {

        miniMapTransform.localPosition = new Vector3(0, 3200, 0);

        
        

        if (Input.GetMouseButton(0))
        {
            navMeshAgent.speed = 0.75f;
            distanceReached = false;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit, 1000f);
            
            target = hit.point;

            
            //navMeshAgent.SetDestination(hit.point);

            
            

    }

        if(target != Vector3.zero)
        {
            distance = Vector3.Distance(transform.position, target);
        }
        else
        {
            float f = Mathf.PingPong(Time.time, 3) - 1.5f;

            //transform.rotation = startRotation * Quaternion.AngleAxis(f, Vector3.forward);
        }

        /*if (distance > 0.1)
        {
            direction = (target - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
            Vector3 movement = transform.forward * Time.deltaTime * speed;
            navMeshAgent.Move(movement);
        }*/

        if(target.x < float.PositiveInfinity)
        {
            Vector3 dir = target - transform.position;
            var newDir = Vector3.RotateTowards(transform.forward, dir, 50 * Time.deltaTime, 0.0f);
            var newRot = Quaternion.LookRotation(newDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 2f);

            if (distance > navMeshAgent.radius + 0.1f)
            {
                Vector3 movement = transform.forward * Time.deltaTime * 2f;
                navMeshAgent.Move(movement);
            }
            else
            {

            }
        }
        
        if (distance <= 1)
        {
            
            distanceReached = true;
        }

        
        if (!distanceReached)
        {
            //RotateShip();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {

            if (!canvasToggled)
            {
                canvasGameObject.SetActive(true);
                canvasToggled = true;
                
            }
            else
            {
                canvasGameObject.SetActive(false);
                canvasToggled = false;
            }

            
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
