using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaparkour : MonoBehaviour
{
    public GameObject[] P;
    public float velocidadplataforma = 2;
    private int PIndex = 0;

    void Update()
    {
       movimientoplataforma();
    }

    void movimientoplataforma()
    {
        if(Vector3.Distance(transform.position, P[PIndex].transform.position) < 0.1f)
        {
            PIndex++;
            if(PIndex >= P.Length)
            {
                PIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, P[PIndex].transform.position, velocidadplataforma*Time.deltaTime);
    }

    // private OnCollisionEnter(Coliision coliision)
    // {
    //     if(coliision.GameObject.CompareTag("Player"))
    //     {
    //         coliision.GameObject,transform.SetParent(transform);
    //     }
    // }

    // private OnCollisionExit(Coliision coliision)
    // {
    //     if(coliision.GameObject.CompareTag("Player"))
    //     {
    //         coliision.GameObject,transform.SetParent(null);
    //     }
    // }
}
