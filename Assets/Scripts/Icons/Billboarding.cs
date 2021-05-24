using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{

    public GameObject icon;

    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        icon.transform.LookAt(icon.transform.position + cam.transform.forward);
    }
}
