using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField] private Transform player;
    void Update()
    {
        this.transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
