using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringCannons : MonoBehaviour
{

    

    [SerializeField] Transform[] RightSideCannons;
    [SerializeField] Transform[] LeftSideCannons;

    public Rigidbody cannonBall;

    public float impulseForce;

    private int rightSideCurrent;

    private int leftSideCurrent;



    // Start is called before the first frame update
    void Start()
    {
        rightSideCurrent = -1;
        leftSideCurrent = -1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            string side = "RightSide";
            rightSideCurrent++;

            if (rightSideCurrent > 2)
            {
                rightSideCurrent = 0;
            }

            Fire(rightSideCurrent, side);
        }

        if (Input.GetKeyDown(KeyCode.A))
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

    private void Fire(int whichCannon, string side)
    {
        if(side == "RightSide")
        {
            Rigidbody projectileInstance;
            projectileInstance = Instantiate(cannonBall, RightSideCannons[whichCannon].position, RightSideCannons[whichCannon].rotation);
            projectileInstance.AddForce(RightSideCannons[whichCannon].forward * impulseForce);
            
        }
        else
        {
            Rigidbody projectileInstance;
            projectileInstance = Instantiate(cannonBall, LeftSideCannons[whichCannon].position, LeftSideCannons[whichCannon].rotation);
            projectileInstance.AddForce(LeftSideCannons[whichCannon].forward * impulseForce);
            
        }
        
    }
}
