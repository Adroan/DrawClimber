using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    private List<Vector3> legPosCells;

    public bool hasLeg {get; set;}


    private GameObject Rleg = null;
    private GameObject Lleg = null;


    public void setLegPosCells(List<Vector3> posCells)
    {
        if(Rleg != null && Lleg != null){
            Destroy(Rleg);
            Destroy(Lleg);
        }

        GameObject leg = new GameObject();
        List<Vector3> posCellsInLeg = new List<Vector3>();

        foreach (Vector3 pos in posCells)
        {
            posCellsInLeg.Add(new Vector3(pos.x, pos.y + 1, 0));
        }


        foreach (Vector3 pos in posCellsInLeg)
        {
            GameObject cel = Instantiate(cell, pos, Quaternion.identity);
            cel.transform.SetParent(leg.transform);
        }

        Rleg = Instantiate(leg,transform.Find("RightLeg").transform.position, Quaternion.identity);
        Rleg.transform.SetParent(transform.Find("RightLeg").transform);
        Rleg.transform.rotation = Quaternion.Euler(0,0,0) ;
        Rleg.transform.name = "Rleg";
        Lleg = Instantiate(leg, transform.Find("LeftLeg").transform.position, Quaternion.identity);
        Lleg.transform.SetParent(transform.Find("LeftLeg").transform);
        Lleg.transform.name ="Lleg";

        Destroy(leg);

        hasLeg = true;
    }


    

    

}
