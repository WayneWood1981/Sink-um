using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyWander : MonoBehaviour
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
    public EnemyChase enemyChase;
    public NavMeshAgent agent;

    public float range = 10;

    public Vector3 newWaypoint;

    public bool hasSetWaypoint;

    private void Start()
    {
        enemyChase = GetComponent<EnemyChase>();

        agent = GetComponent<NavMeshAgent>();
        Wander();
    }

    private void Update()
    {
        currentTimer += 1 * Time.deltaTime;

        if (enemyChase.shipTarget == null && health.isDead == false)
        {
            if (currentTimer >= maxTimer)
            {

                //m_Agent.destination = m_HitInfo.point;
                m_Path = new NavMeshPath();

                enemAgent.CalculatePath(Wander(),
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
            if (!health.isDead)
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

    private Vector3 Wander()
    {
        if (!hasSetWaypoint)
        {
            float x = Random.Range(transform.position.x - range, transform.position.x + range);
            float y = 0.43f;
            float z = Random.Range(transform.position.z - range, transform.position.z + range);

            newWaypoint = new Vector3(x, y, z);
            hasSetWaypoint = true;

            return newWaypoint;
        }

        return Vector3.zero;
    }
}
