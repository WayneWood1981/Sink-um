using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitting : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}