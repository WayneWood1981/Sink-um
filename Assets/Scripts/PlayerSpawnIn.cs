using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnIn : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;

    private Transform playerSpawnPoint;

    private void Awake()
    {
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNextPlayer()
    {
        GameObject newPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);


        FindObjectOfType<CameraFollow>().FindPlayer();
    }
    void PlayerInstantiate()
    {
        
            
            
        

    }
}
