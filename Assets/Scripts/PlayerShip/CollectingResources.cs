using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectingResources : MonoBehaviour
{

    public float playersGold;
    public float playersMaxGold;
    public Text goldText;

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
        
    }
    private void Start()
    {
        playersMaxGold = goldSlider.maxValue;
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
        healthText.text = Mathf.RoundToInt(playersHealth).ToString();


        playersNotoriety = playersGold / 1f; // make it 100 for the game


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
            playersGold += 1000f;
            //play sound
            other.gameObject.SetActive(false);
            Destroy(other.gameObject, 2);
            
        }else if (other.transform.tag == "CannonBalls")
        {
            playerscannonBalls += 5;
            other.gameObject.SetActive(false);
            Destroy(other.gameObject, 2);
            
        }
        else if (other.transform.tag == "HealthBarrel")
        {
            playersHealth += 20f;
            other.gameObject.SetActive(false);
            Destroy(other.gameObject, 2);
            
        }
        
    }
}
