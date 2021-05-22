using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{

    [SerializeField] Transform destructibles;
    [SerializeField] Transform boat;

    NavMeshAgent navmesh;
    CreateLootFromShip lootFromShip;
    EnemyMovement enemyMovement;

    public float maxHealth = 100;
    public float currentHealth;

    private Quaternion lookRotation;
    private Vector3 direction;
    public float radius = 5.0F;
    public float power = 10.0F;
    private float RotationSpeed;
    private float movementSpeed;

    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        navmesh = GetComponent<NavMeshAgent>();
        lootFromShip = FindObjectOfType<CreateLootFromShip>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if (isDead == false)
            {
                navmesh.speed = 0.0f;
                navmesh.updateRotation = false;
                Die();
            }

            Rigidbody rb = boat.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            SphereCollider[] colls = boat.GetComponents<SphereCollider>();
            NavMeshAgent nma = boat.GetComponent<NavMeshAgent>();
            nma.enabled = false;
            foreach (var col in colls)
            {
                col.enabled = false;

            }
            rb.detectCollisions = false;
            boat.transform.position += new Vector3(0, -5f, 0) * 0.025f * Time.deltaTime;
            Destroy(boat.gameObject, 5);
               
            
        }
        
    }

    private void Die()
    {
        isDead = true;
        lootFromShip.CreateLoot(boat, enemyMovement.shipSize);
        foreach (Transform child in destructibles.transform)
        {
            Vector3 explosionPos = child.position;
            Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
            rb.mass = 100f;
            SphereCollider sc = child.gameObject.AddComponent<SphereCollider>();

            

            
        }
    }
}
