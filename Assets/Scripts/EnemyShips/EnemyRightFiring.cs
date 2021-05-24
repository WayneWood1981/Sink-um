using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRightFiring : MonoBehaviour
{
    [SerializeField] Transform[] RightSideCannons;

    Health health;

    public Rigidbody cannonBall;

    public float impulseForce;

    private int rightSideCurrent;

    private bool allowedToFire;

    private float currentTime;

    public float cannonCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        rightSideCurrent = -1;
        cannonCoolDown = 1.5f;
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
            rightSideCurrent++;
            if (rightSideCurrent > 2)
            {
                rightSideCurrent = 0;
            }
            Rigidbody projectileInstance;
            projectileInstance = Instantiate(cannonBall, RightSideCannons[rightSideCurrent].position, RightSideCannons[rightSideCurrent].rotation);
            projectileInstance.AddForce(RightSideCannons[rightSideCurrent].forward * impulseForce);
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
