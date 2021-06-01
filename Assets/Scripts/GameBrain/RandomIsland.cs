using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIsland : MonoBehaviour
{

    public Transform[] islands;
    public Sprite chest;

    int Rand;
    int length = 9;
    int numberOfIslands = 19;
    List<int> chestIslandList = new List<int>();

    private void Awake()
    {
        // Disable every Box Collider on the map
        
        foreach (var island in islands)
        {
            // Change miniMap icon and enable box collider.
            BoxCollider islandBoxCollider = island.GetComponent<BoxCollider>();
            islandBoxCollider.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

        chestIslandList = new List<int>(new int[length]);

        for (int i = 0; i < length; i++)
        {
            Rand = Random.Range(0, numberOfIslands);

            while (chestIslandList.Contains(Rand))
            {
                Rand = Random.Range(0, numberOfIslands);
            }

            chestIslandList[i] = Rand;
            
        }

        

        foreach (var number in chestIslandList)
        {
            // Change miniMap icon and enable box collider.
            islands[number].GetComponent<BoxCollider>().enabled = true;

            SpriteRenderer islandIcon = islands[number].GetComponentInChildren<SpriteRenderer>();
            islandIcon.sprite = chest;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
