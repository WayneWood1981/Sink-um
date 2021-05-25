using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIconDirection : MonoBehaviour
{

    public Transform mapIcon;

    Quaternion iniRot;
    // Start is called before the first frame update
    void Start()
    {
        iniRot = mapIcon.transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mapIcon.transform.rotation = iniRot;

    }

    

    
        
    
}
