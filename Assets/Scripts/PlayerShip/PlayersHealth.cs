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
    [SerializeField] GameObject playersLoot;

    public CollectingResources playersResources;
    public EnemyChase enemyChase;
    

    public Transform cam;

    public GameObject fireBack;
    public GameObject fireFront;
    public Transform playerSpawnPoint;
    public Transform player;
    public Image fadeToBlackImage;

    public Transform currentSprite;
    public Sprite shipWreckSprite;

    public Slider slider;
    public Text healthCount;

    NavMeshAgent navmesh;

    public float maxHealth = 100;
    public float currentHealth;

    private bool isDead;
    private bool startFading;
    private bool hasInstantiated;
    

    private void Awake()
    {
        GameObject hs = GameObject.Find("Health Slider");
        GameObject ht = GameObject.Find("Health Count");
        GameObject ftb = GameObject.Find("FadeToBlack Canvas");
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;

        fadeToBlackImage = ftb.GetComponentInChildren<Image>();
        slider = hs.GetComponent<Slider>();
        healthCount = ht.GetComponent<Text>();
        cam = GameObject.Find("Main Camera").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        navmesh = GetComponent<NavMeshAgent>();
        fireBack.SetActive(false);
        fireFront.SetActive(false);
        enemyChase = FindObjectOfType<EnemyChase>();
    }

    // Update is called once per frame
    void Update()
    {

        slider.value = currentHealth;
        healthCount.text = currentHealth.ToString();

        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth > 70)
        {
            fireBack.SetActive(false);
            fireFront.SetActive(false);
        }else if (currentHealth <= 70 && currentHealth > 30)
        {
            fireBack.SetActive(true);
            fireFront.SetActive(false);

        }
        else if (currentHealth <= 30 && currentHealth > 0)
        {
            fireBack.SetActive(true);
            fireFront.SetActive(true);
        }else if (currentHealth <= 0)
        {
            if (isDead == false)
            {
                currentHealth = 0;
                navmesh.speed = 0.0f;
                navmesh.updateRotation = false;
                navmesh.isStopped = true;
                Die();
            }
        }

        
    }

    private void LateUpdate()
    {
        
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

            Destroy(child.gameObject, 2.5f);


            

            

        }
        PrepareNextPlayer();
        if(playersResources.playersGold > 0)
        {
            FindObjectOfType<CreateLootFromShip>().createPlayersLoot(this.transform);
        }
        
        //enemyChase.hasRestoredTargetList = false;
    }

    void PrepareNextPlayer()
    {
        
        navmesh.enabled = false;
        GetComponent<FiringCannons>().enabled = false;
        GetComponent<CollectingResources>().enabled = false;
        GetComponent<ClickToSteer>().enabled = false;
        GetComponent<Rigidbody>().detectCollisions = false;
        currentSprite.GetComponent<SpriteRenderer>().sprite = shipWreckSprite;
        startFading = true;
        FadeToBlack();
        Invoke("ChangeTag", 2);
        
        
    }

    void ChangeTag()
    {
        this.transform.tag = "ShipWreck";
        this.transform.name = "ShipWreck";
        Destroy(this.transform.Find("EnemyTarget1").gameObject);
        Destroy(this.transform.Find("EnemyTarget2").gameObject);
        Destroy(this.transform.Find("EnemyTarget3").gameObject);
        Destroy(this.transform.Find("EnemyTarget4").gameObject);
        Destroy(this.transform.Find("EnemyTarget5").gameObject);
        Destroy(this.transform.Find("EnemyTarget6").gameObject);
        Destroy(this.transform.Find("EnemyTarget7").gameObject);
        Destroy(this.transform.Find("EnemyTarget8").gameObject);
        FindObjectOfType<PlayerSpawnIn>().SpawnNextPlayer();
        this.enabled = false;
        
    }

    void FadeToBlack()
    {
        fadeToBlackImage.color = Color.black;
        fadeToBlackImage.canvasRenderer.SetAlpha(0.0f);
        fadeToBlackImage.CrossFadeAlpha(1.0f, 3, false);
        

    }

    
}
