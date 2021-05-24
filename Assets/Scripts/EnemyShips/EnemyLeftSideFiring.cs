using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeftSideFiring : MonoBehaviour
{
    [SerializeField] Transform[] LeftSideCannons;

    Health health;

    public Rigidbody cannonBall;

    public float impulseForce;

    private int leftSideCurrent;

    private bool allowedToFire;

    private float currentTime;

    public float cannonCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        leftSideCurrent = -1;
        cannonCoolDown = 0.75f;
        currentTime = 0;
        allowedToFire = true;
        health = GetComponentInParent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (allowedToFire == false)
        {
            currentTime += 1 * Time.deltaTime;

            if (currentTime >= cannonCoolDown)
            {
                allowedToFire = true;
                currentTime = 0;
            }
        }
    }

    private void Fire()
    {
        if (allowedToFire && health.isDead == false)
        {
            leftSideCurrent++;
            if (leftSideCurrent > 2)
            {
                leftSideCurrent = 0;
            }
            Rigidbody projectileInstance;
            projectileInstance = Instantiate(cannonBall, LeftSideCannons[leftSideCurrent].position, LeftSideCannons[leftSideCurrent].rotation);
            projectileInstance.AddForce(LeftSideCannons[leftSideCurrent].forward * impulseForce);
            allowedToFire = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            
            
            Fire();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            
        }
    }
}
