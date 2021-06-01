using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{

    public Transform playersShip;

    public Image fadeToWhiteImage;

    public Vector3 offset;

    private bool toggleZoom;

    private Vector3 offSetDefault;

    private Vector3 zoomedOutVector;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
        offSetDefault = offset;
        FindPlayer();
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        playersShip = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = playersShip.position + offset;
        
        

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!toggleZoom)
            {
                
                offset = new Vector3(43.4f,37.3f, 0.53f);
                toggleZoom = true;
            }
            else
            {
                offset = offSetDefault;
                toggleZoom = false;
            }
        }
    }

    void FadeFromBlack()
    {
        
        fadeToWhiteImage.color = Color.black;
        fadeToWhiteImage.canvasRenderer.SetAlpha(1.0f);
        fadeToWhiteImage.CrossFadeAlpha(0.0f, 3, false);
    }

    public void FindPlayer()
    {
        playersShip = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = playersShip.position + offset;
        FadeFromBlack();
    }


}
