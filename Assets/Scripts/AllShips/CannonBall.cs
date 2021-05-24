using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{

    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().currentHealth -= damage;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.SetActive(false);
            Destroy(gameObject, 2);
        }
    }


}
