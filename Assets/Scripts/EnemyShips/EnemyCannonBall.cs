using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBall : MonoBehaviour
{
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayersHealth>().currentHealth -= damage;

            gameObject.SetActive(false);
            Destroy(gameObject, 2);
        }
    }
}
