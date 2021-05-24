using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayersHealth : MonoBehaviour
{

    [SerializeField] Transform destructibles;
    [SerializeField] Transform boat;

    public GameObject fireBack;
    public GameObject fireFront;

    public Slider slider;
    public Text healthCount;

    NavMeshAgent navmesh;

    public float maxHealth = 100;
    public float currentHealth;

    private bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        navmesh = GetComponent<NavMeshAgent>();
        fireBack.SetActive(false);
        fireFront.SetActive(false);
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
                navmesh.isStopped = true;
                Die();
            }
        }else if (currentHealth <= 70)
        {
            fireBack.SetActive(true);
        }else if (currentHealth <= 30)
        {
            fireFront.SetActive(true);
        }else if (currentHealth > 70)
        {
            fireFront.SetActive(false);
            fireFront.SetActive(false);
        }
        {

        }
    }

    private void LateUpdate()
    {
        slider.value = currentHealth;
        healthCount.text = currentHealth.ToString();
    }

    private void Die()
    {
        isDead = true;
        foreach (Transform child in destructibles.transform)
        {
            Vector3 explosionPos = child.position;
            Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
            rb.mass = 100f;
            SphereCollider sc = child.gameObject.AddComponent<SphereCollider>();
            sc.radius = 5;

            Destroy(child.gameObject, 2);


        }
    }
}
