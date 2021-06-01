using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSearch : MonoBehaviour
{
    
    public GameObject floor;
    public GameObject bossFloor;
    
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Boss"))
        {

            floor.GetComponent<MeshRenderer>().enabled = false;
            
            
        }
        else
        {
            floor.GetComponent<MeshRenderer>().enabled = true;

        }
        


        
    }

}
