using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectingResources : MonoBehaviour
{

    AudioSource audioSource;

    public AudioClip collectGoldChest;
    public AudioClip collectHealth;
    public AudioClip collectCannonBalls;

    public float playersGold;
    public float playersMaxGold;
    public Text goldText;

    public int healthBarrelHealth;
    public int goldBarrelGold;
    public int cannonBallBarrelBall;

    public float playersHealth;
    public float playersMaxHealth = 100f;
    public Text healthText;

    public float playersNotoriety;
    public float playersMaxNotoriety = 100f;
    public Text notorietyText;

    public int playerscannonBalls;
    public int playersMaxBalls = 50;
    public Text cannonBallText;

    public Slider goldSlider;
    public Slider healthSlider;
    public Slider NotorietySlider;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        playersMaxGold = goldSlider.maxValue;
        playersMaxHealth = healthSlider.maxValue;
        playerscannonBalls = playersMaxBalls;
        cannonBallText.text = playersMaxBalls.ToString();
        playersHealth = playersMaxHealth;
    }

    private void Update()
    {
        goldSlider.value = playersGold;
        goldText.text = Mathf.RoundToInt(playersGold).ToString();

        NotorietySlider.value = playersNotoriety;
        notorietyText.text = Mathf.RoundToInt(playersNotoriety).ToString();

        healthSlider.value = playersHealth;
        healthText.text = Mathf.RoundToInt(playersHealth).ToString() + "%";


        playersNotoriety = playersGold / 100f; // make it 100 for the game


        cannonBallText.text = playerscannonBalls.ToString();

        if (NotorietySlider.value > NotorietySlider.maxValue)
        {
            NotorietySlider.value = NotorietySlider.maxValue;
        }

        if (playerscannonBalls > playersMaxBalls)
        {
            playerscannonBalls = playersMaxBalls;
        }

        if (playersHealth > playersMaxHealth)
        {
            playersHealth = playersMaxHealth;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {



        if (other.transform.tag == "Chest")
        {
            playersGold += goldBarrelGold;
            
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            audioSource.PlayOneShot(collectGoldChest, 0.4f);
            Destroy(other.gameObject, 5);
            
        }else if (other.transform.tag == "CannonBalls")
        {
            playerscannonBalls += cannonBallBarrelBall;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            audioSource.PlayOneShot(collectCannonBalls, 0.4f);
            Destroy(other.gameObject, 5);
            
        }
        else if (other.transform.tag == "HealthBarrel")
        {
            playersHealth += healthBarrelHealth;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            audioSource.PlayOneShot(collectHealth, 0.4f);
            Destroy(other.gameObject, 5);
            


        }
        
    }
}
