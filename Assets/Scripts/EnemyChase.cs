using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChase : MonoBehaviour
{

    public float range;

    public Transform cube;

    public Vector3 newWaypoint;

    public bool hasSetWaypoint;

    public bool isPatrolling;

    public float SPEED;

    NavMeshAgent m_Agent;

    NavMeshPath m_Path;

    

    Health health;

    int pathIter = 1;

    public List<Transform> targets = new List<Transform>();

    Vector3 AgentPosition;
    
    Vector3 destination =
        new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
    public Transform shipTarget;
    

    public float currentTimer;
    public float maxTimer = 3f;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Path != null && m_Path.corners != null && m_Path.corners.Length > 0)
        {
            var prev = AgentPosition;
            for (int i = pathIter;
                i < m_Path.corners.Length; ++i)
            {
                Gizmos.DrawLine(prev, m_Path.corners[i]);
                prev = m_Path.corners[i];
            }
        }
    }

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.isStopped = true;
        m_Path = new NavMeshPath();
        health = GetComponent<Health>();
        isPatrolling = true;
        PopulateTheListWithTargets();
    }

    void Update()
    {
        if (isPatrolling)
        {
            Wander();
            //RandomNavSphere(transform.position, range);
        }
        
        SetAgentPosition();
        currentTimer += 1 * Time.deltaTime;

        if (health.isDead == false)
        {
            if (currentTimer >= maxTimer)
            {
               
                m_Agent.destination = shipTarget.position;
                m_Path = new NavMeshPath();

                
                m_Agent.CalculatePath(shipTarget.position,
                                      m_Path);
                pathIter = 1;
                m_Agent.isStopped = false;
                currentTimer = 0;
            }
        }

        
        

        if (m_Path.corners == null || m_Path.corners.Length == 0)
            return;


        if (pathIter >= m_Path.corners.Length)
        {
            if (!health.isDead)
            {
                destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                m_Agent.isStopped = true;
                return;
            }
        }
        else
        {
            destination = m_Path.corners[pathIter];
        }

        if (destination.x < float.PositiveInfinity)
        {
            Vector3 direction = destination - AgentPosition;
            var newDir =
                Vector3.RotateTowards(transform.forward,
                    direction,
                    50 * Time.deltaTime, 0.0f);
            var newRot = Quaternion.LookRotation(newDir);
            transform.rotation =
                Quaternion.Slerp(transform.rotation,
                                 newRot, Time.deltaTime * SPEED);

            float distance =
                Vector3.Distance(AgentPosition, destination);

            if (distance > m_Agent.radius + 0.1)
            {
                
                Vector3 movement =
                    transform.forward * Time.deltaTime * SPEED;
                
                m_Agent.Move(movement);
            }
            else
            {
                
                ++pathIter;
                if (pathIter >= m_Path.corners.Length)
                {
                    destination =
                        new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                    m_Agent.isStopped = true;

                    //IF WANDERING

                    hasSetWaypoint = false;
                    
                }
            }
        }
    }

    public Transform getTargetDistances(List<Transform> targets)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in targets)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    void SetAgentPosition()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position,
                                  out hit, 1.0f,
                                  NavMesh.AllAreas))
        {
            AgentPosition = hit.position;
        }
    }

    private void Wander()
    {
        if (!hasSetWaypoint)
        {
            range = Random.Range(10, 30);
            float x = Random.Range(transform.position.x - range, transform.position.x + range);
            float y = 0.43f;
            float z = Random.Range(transform.position.z - range, transform.position.z + range);

            newWaypoint = new Vector3(x, y, z);

            Transform newCube = Instantiate(cube, newWaypoint, Quaternion.identity);

            shipTarget = newCube;

            hasSetWaypoint = true;

            Destroy(newCube.gameObject, 20);
            
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




        // Cant get this to work!!
        /*for (int i = 0; i < 8; i++)
        {
            GameObject target = GameObject.Find("EnemyTarget" + i);

            Debug.Log(target.name);
            targets.Add(target.transform);
        }*/
    }
}