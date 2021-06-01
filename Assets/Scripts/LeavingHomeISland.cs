using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavingHomeISland : MonoBehaviour
{
    public GameObject gameBrain;

    public bool isOnIsland;

    private void Start()
    {
        gameBrain = GameObject.Find("GameBrain");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isOnIsland = false;
            
            gameBrain.GetComponentInChildren<PlayerSpawnIn>().instructCanvas.SetActive(false);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isOnIsland = true;
        }
    }

    
}
