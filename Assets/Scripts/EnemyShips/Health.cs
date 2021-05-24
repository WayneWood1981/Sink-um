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
    public CreateLootFromShip lootFromShip;
    EnemyMovement enemyMovement;

    public float maxHealth = 100;
    public float currentHealth;
    public int shipSize;

    private bool droppingLoot;



    public float radius = 5.0F;
    public float power = 10.0F;
    
    public bool isDead;
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
                
                Die();
            }

            Rigidbody rb = boat.GetComponent<Rigidbody>();
            //rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            SphereCollider[] colls = boat.GetComponents<SphereCollider>();
            NavMeshAgent nma = boat.GetComponent<NavMeshAgent>();
            nma.enabled = false;
            foreach (var col in colls)
            {
                col.enabled = false;

            }
            //rb.detectCollisions = false;
            boat.transform.position += new Vector3(0, -5f, 0) * 0.025f * Time.deltaTime;
            Destroy(boat.gameObject, 5);
               
            
        }
        
    }

    private void Die()
    {
        isDead = true;

        foreach (Transform child in destructibles.transform)
        {
            child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            SphereCollider sc = child.gameObject.AddComponent<SphereCollider>();
            sc.radius = 5;

            Destroy(child.gameObject, 2);
        }

        if (!droppingLoot)
        {
            lootFromShip.CreateLoot(boat, shipSize);
            droppingLoot = true;
        }

    }

    
}
