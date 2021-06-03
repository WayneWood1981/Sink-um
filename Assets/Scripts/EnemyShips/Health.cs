using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] Transform destructibles;
    [SerializeField] Transform boat;

    public Slider healthSlider;

    public AudioClip death;
    private bool playDeath;

    public GameObject fireBack;
    public GameObject fireFront;

    NavMeshAgent navmesh;
    AudioSource audioSource;
    

    public float maxHealth = 100;
    public float currentHealth;
    public int shipSize;

    private bool droppingLoot;

    public CreateLootFromShip lootFromShip;

    public float radius = 5.0F;
    public float power = 10.0F;
    
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        navmesh = GetComponent<NavMeshAgent>();
        lootFromShip = FindObjectOfType<CreateLootFromShip>();
        
        audioSource = GetComponent<AudioSource>();

        healthSlider.maxValue = maxHealth;

        fireBack.SetActive(false);
        fireFront.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            if (isDead == false)
            {
                
                Die();
                if (!playDeath)
                {
                    audioSource.PlayOneShot(death, 0.2f);
                    playDeath = true;
                }
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
        else if (currentHealth <= currentHealth / 100 * 70)
        {
            fireBack.SetActive(true);
        }
        else if (currentHealth <= currentHealth / 100 * 30)
        {
            fireFront.SetActive(true);
        }
        else if (currentHealth > currentHealth / 100 * 70)
        {
            fireFront.SetActive(false);
            fireFront.SetActive(false);
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
