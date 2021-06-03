using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{

    public AudioClip click;
    public Text storyText;
    public GameObject theStoryButton;
    public GameObject playButton;
    public GameObject splashMenu;

    public Image splash;
    public Image Dice;
    public Image Title;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("Fade", 2);
        

    }

    public void playPushed()
    {
        audioSource.PlayOneShot(click);
        playButton.SetActive(false);
        theStoryButton.SetActive(true);
        Invoke("MoveToNextScene", 2);
    }

    public void changeStoryText()
    {
        audioSource.PlayOneShot(click);
        theStoryButton.SetActive(false);
        storyText.text = "\n\n Whilst searching a dark cavern in the southern shores of\n'Dead mans Grog'\nyou find a glowing chest that contains a map to the lost treasure" +
            " of 'SeaLegs Solomon' the craziest blood thirstiest Pirate ever to have sailed the seas." +
            "\nSearch for the treasure if you dare, but be warned.... \n'Sealegs' WILL rise from his watery grave to hunt whoever takes his plunder.";
    }

    void MoveToNextScene()
    {
        SceneManager.LoadScene(1);
    }

    void Fade()
    {
        FadeFromBlack();
        Invoke("FadeBG", 4);
    }
    void FadeBG()
    {
        FadeFromBlackBG();
    }

    void FadeFromBlack()
    {

        
        Dice.canvasRenderer.SetAlpha(1.0f);
        Title.canvasRenderer.SetAlpha(1.0f);

        
        Dice.CrossFadeAlpha(0.0f, 3, false);
        Title.CrossFadeAlpha(0.0f, 3, false);
    }

    void FadeFromBlackBG()
    {
        splash.color = Color.black;
        splash.canvasRenderer.SetAlpha(1.0f);
        splash.CrossFadeAlpha(0.0f, 3, false);
        Invoke("setactive", 3);
    }

    void setactive()
    {
        splashMenu.SetActive(false);
    }

}
