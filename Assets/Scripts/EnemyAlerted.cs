using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Use physics raycast hit from mouse click 
// to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAlerted : MonoBehaviour
{


    

    public float SPEED;

    NavMeshAgent enemAgent;

    NavMeshPath m_Path;

    Health health;

    int pathIter = 1;

    public List<Transform> targets = new List<Transform>();

    Vector3 AgentPosition;

    Vector3 destination =
        new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
    public Transform shipTarget;
    RaycastHit m_HitInfo = new RaycastHit();

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
        enemAgent = GetComponent<NavMeshAgent>();
        enemAgent.isStopped = true;
        m_Path = new NavMeshPath();
        health = GetComponent<Health>();
        
        PopulateTheListWithTargets();
    }

    void Update()
    {

        

        SetAgentPosition();

        currentTimer += 1 * Time.deltaTime;

        if (shipTarget != null && health.isDead == false)
        {
            if (currentTimer >= maxTimer)
            {

                //m_Agent.destination = m_HitInfo.point;
                m_Path = new NavMeshPath();
                
                enemAgent.CalculatePath(shipTarget.position,
                                         m_Path);
                pathIter = 1;
                enemAgent.isStopped = false;


                currentTimer = 0;
            }
        }
        
        if (m_Path.corners == null || m_Path.corners.Length == 0)
            return;


        if (pathIter >= m_Path.corners.Length)
        {
            if(!health.isDead)
            destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            enemAgent.isStopped = true;
            return;
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

            if (distance > enemAgent.radius + 0.1)
            {
                Vector3 movement =
                    transform.forward * Time.deltaTime * SPEED;

                //if not dead
                if (!health.isDead)
                {
                    enemAgent.Move(movement);
                }
                
            }
            else
            {
                ++pathIter;
                if (pathIter >= m_Path.corners.Length)
                {
                    //here!!
                    //destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

                    
                    enemAgent.isStopped = true;
                }
            }
        }
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

    public Transform getTargetDistances(List<Transform> possTargets)
    {
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < possTargets.Count; i++)
        {
            float distance = Vector3.Distance(targets[i].transform.position, transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                
                //shipTarget = targets[i];

                return targets[i];
            }
        }
        return null;
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
