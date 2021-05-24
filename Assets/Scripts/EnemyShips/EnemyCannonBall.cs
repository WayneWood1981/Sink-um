using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBall : MonoBehaviour
{
    public float damage;

    public GameObject hitSmash;

    private ContactPoint collisionPoint;


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

            collisionPoint = collision.GetContact(0);



            GameObject temporarySplatMarkHandler;

            temporarySplatMarkHandler = Instantiate(hitSmash, collisionPoint.point, Quaternion.LookRotation(collisionPoint.normal)) as GameObject;


            temporarySplatMarkHandler.transform.Rotate(Vector3.right / 70);
            temporarySplatMarkHandler.transform.Rotate(Vector3.forward * 90);
            temporarySplatMarkHandler.transform.Translate(Vector3.up * 0.005f);
        }
        if (collision.transform.tag == "Enemy")
        {
            gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        }
    }
}
