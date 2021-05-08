using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField] Vector3 position;

    [SerializeField] Transform player;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        player.position = position; 
    }
}
