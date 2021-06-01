using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectingResources : MonoBehaviour
{

    AudioSource audioSource;
    PlayersHealth playersHealth;

    public AudioClip collectGoldChest;
    public AudioClip collectHealth;
    public AudioClip collectCannonBalls;

    public float playersGold;
    public float playersMaxGold;

    
    public Text goldText;


    public int healthBarrelHealth;
    public int goldBarrelGold;
    public int cannonBallBarrelBall;

    
    

    public float playersNotoriety;
    public float playersMaxNotoriety = 100f;
    public Text notorietyText;

    public int playerscannonBalls;
    public int playersMaxBalls = 50;
    public Text cannonBallText;

    public Slider goldSlider;
   
    public Slider NotorietySlider;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject gT = GameObject.Find("Gold Count");
        
        GameObject nT = GameObject.Find("Notoriety Count");
        GameObject bT = GameObject.Find("Ball Count");
        GameObject gS = GameObject.Find("Gold Slider");
        
        GameObject nS = GameObject.Find("Notoriety Slider");
        

        goldText = gT.GetComponent<Text>();
        
        notorietyText = nT.GetComponent<Text>();
        cannonBallText = bT.GetComponent<Text>();
        goldSlider = gS.GetComponent<Slider>();
        
        NotorietySlider = nS.GetComponent<Slider>();

    }
    private void Start()
    {
        playersMaxGold = goldSlider.maxValue;
        playersHealth = GetComponent<PlayersHealth>();
        playerscannonBalls = playersMaxBalls;
        cannonBallText.text = playersMaxBalls.ToString();
        

        // Create Player Instances

        
        

    }

    private void Update()
    {
        goldSlider.value = playersGold;
        goldText.text = Mathf.RoundToInt(playersGold).ToString();

        NotorietySlider.value = playersNotoriety;
        notorietyText.text = Mathf.RoundToInt(playersNotoriety).ToString();

        


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

       
    }

    
    private void OnTriggerEnter(Collider other)
    {



        if (other.transform.tag == "PlayersLoot")
        {
            
            playersGold = other.gameObject.GetComponent<playersLootChests>().amountInChest;
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
            playersHealth.currentHealth += healthBarrelHealth;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            audioSource.PlayOneShot(collectHealth, 0.4f);
            Destroy(other.gameObject, 5);
            


        }
        
    }
}
