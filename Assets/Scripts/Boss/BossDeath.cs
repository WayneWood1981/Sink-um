using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{

    Health bossHealth;
    CollectingResources playersResources;

    public GameObject GameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        bossHealth = GetComponent<Health>();
        playersResources = FindObjectOfType<CollectingResources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth.currentHealth <= 0 && playersResources.playersGold >= playersResources.playersMaxGold)
        {
            Instantiate(GameOverCanvas);
        }
    }
}
