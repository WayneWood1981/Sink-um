using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform player;

    Vector3 direction;

    private void Update()
    {
        direction.z = player.localEulerAngles.y;

        transform.localEulerAngles = direction;
    }
}
