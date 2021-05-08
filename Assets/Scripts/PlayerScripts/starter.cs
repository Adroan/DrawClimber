using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starter : MonoBehaviour
{
   [SerializeField] private Player player;
    void Update()
    {
        if(player.hasLeg){
            Destroy(this.gameObject);
        }
    }
}
