using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRocking : MonoBehaviour
{

    private bool objectHasReachedHeight;
    public float speed;

    private float originalYPos;

    // Start is called before the first frame update
    void Start()
    {
        originalYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float z = transform.position.z;
        float y = transform.position.y;
        


    }
}
