using Random = UnityEngine.Random;
using UnityEngine;

public class CreateLootFromShip : MonoBehaviour
{
    public Transform[] loot;

    

    public float  speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
            
        
    }

    public void CreateLoot(Transform whereShipSunk, int shipSize)
    {
        

        for (int i = 0; i < shipSize; i++)
        {
            
            Transform shipsLoot = Instantiate(loot[Random.Range(0, loot.Length)], whereShipSunk.position, whereShipSunk.rotation);
            shipsLoot.GetComponent<Rigidbody>().AddForce(transform.up * speed);
        }
        
        

    }

    
}
