using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPosition : MonoBehaviour
{

    public Transform mapIcon;
    // Start is called before the first frame update
    void Start()
    {
        mapIcon.localPosition = new Vector3(0, 1200, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
