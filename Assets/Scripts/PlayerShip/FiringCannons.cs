using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringCannons : MonoBehaviour
{

    

    [SerializeField] Transform[] RightSideCannons;
    [SerializeField] Transform[] LeftSideCannons;

    public CollectingResources playersResources;

    public Rigidbody cannonBall;

    public float impulseForce;

    private int rightSideCurrent;

    private int leftSideCurrent;

    private float currentTime;

    public float cannonCoolDown;

    private bool allowedToFire;

    // Start is called before the first frame update
    void Start()
    {
        rightSideCurrent = -1;
        leftSideCurrent = -1;
        playersResources = GetComponent<CollectingResources>();
        allowedToFire = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (playersResources.playerscannonBalls > 0)
            {
                string side = "RightSide";
                rightSideCurrent++;

                if (rightSideCurrent > 2)
                {
                    rightSideCurrent = 0;
                }

                Fire(rightSideCurrent, side);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (playersResources.playerscannonBalls > 0)
            {
                string side = "leftSide";
                leftSideCurrent++;

                if (leftSideCurrent > 2)
                {
                    leftSideCurrent = 0;
                }
                Fire(leftSideCurrent, side);

            }


        }

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

    private void Fire(int whichCannon, string side)
    {

        if (allowedToFire)
        {
            playersResources.playerscannonBalls -= 1;
            if (side == "RightSide")
            {
                Rigidbody projectileInstance;
                projectileInstance = Instantiate(cannonBall, RightSideCannons[whichCannon].position, RightSideCannons[whichCannon].rotation);
                projectileInstance.AddForce(RightSideCannons[whichCannon].forward * impulseForce);
                allowedToFire = false;
            }
            else
            {
                Rigidbody projectileInstance;
                projectileInstance = Instantiate(cannonBall, LeftSideCannons[whichCannon].position, LeftSideCannons[whichCannon].rotation);
                projectileInstance.AddForce(LeftSideCannons[whichCannon].forward * impulseForce);
                allowedToFire = false;
            }
        }
        
        
    }
}
