using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPosition : MonoBehaviour
{

    public Transform mapIcon;

    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        mapIcon.localPosition = new Vector3(0, 20, 0);
    }

    // Update is called once per frame
    void Update()
    {
        mapIcon.transform.LookAt(cam.position + cam.forward);
    }
}
