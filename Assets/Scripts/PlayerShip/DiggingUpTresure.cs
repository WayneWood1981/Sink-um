using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingUpTresure : MonoBehaviour
{

    public GameObject treasureChest;
    public GameObject openTreasureChest;

    public Vector3 treasureChestStartingPos;
    // Start is called before the first frame update
    void Start()
    {
        treasureChestStartingPos = treasureChest.transform.position;
    }

    
}
