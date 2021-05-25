using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;

    public Vector3 offset;

    private bool toggleZoom;

    private Vector3 offSetDefault;

    private Vector3 zoomedOutVector;


    // Start is called before the first frame update
    void Start()
    {
        offSetDefault = offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!toggleZoom)
            {
                toggleZoom = true;
                offset = new Vector3(52,46, -0.67f);
            }
            else
            {
                offset = offSetDefault;
                toggleZoom = false;
            }
        }
    }

        
}
