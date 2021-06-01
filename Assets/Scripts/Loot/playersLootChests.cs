using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersLootChests : MonoBehaviour
{

    CollectingResources playersResources;

    public float amountInChest;

    private void Awake()
    {
        playersResources = FindObjectOfType<CollectingResources>();
    }
    // Start is called before the first frame update
    void Start()
    {
        amountInChest = playersResources.playersGold;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
