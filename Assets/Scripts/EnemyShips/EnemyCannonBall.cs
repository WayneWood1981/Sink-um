using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBall : MonoBehaviour
{
    public float damage;

    public AudioClip[] splashes;

    public AudioClip[] boatHits;

    public AudioClip[] cannonFire;

    AudioSource audioSource;

    MeshRenderer meshRenderer;

    Rigidbody rb;

    public GameObject splash;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        meshRenderer = GetComponent<MeshRenderer>();

        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);

        audioSource.PlayOneShot(cannonFire[Random.Range(0, cannonFire.Length)], 0.1f);
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayersHealth>().currentHealth -= damage;

            meshRenderer.enabled = false;

            rb.detectCollisions = false;

            GameObject woodGO = collision.transform.Find("WoodImpacts").gameObject;

            ParticleSystem theHit = woodGO.GetComponentInChildren<ParticleSystem>();

            audioSource.PlayOneShot(boatHits[Random.Range(0, boatHits.Length)], 0.1f);

            doEmit(theHit);

        }
        else if (collision.transform.tag == "Sea")
        {
            meshRenderer.enabled = false;

            ContactPoint hitPoint = collision.GetContact(0);



            GameObject splashMiss = Instantiate(splash, hitPoint.point, Quaternion.identity) as GameObject;

            splashMiss.transform.Rotate(90.0f, 0f, 0f, Space.Self);


            if (audioSource)
            {
                audioSource.volume = 20;
                audioSource.PlayOneShot(splashes[Random.Range(0, splashes.Length)], 0.05f);
            }
        }

    }

    void doEmit(ParticleSystem ps)
    {
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.startColor = Color.red;
        emitParams.startSize = 0.2f;
        ps.Emit(emitParams, 1);
        ps.Play();
    }
}
