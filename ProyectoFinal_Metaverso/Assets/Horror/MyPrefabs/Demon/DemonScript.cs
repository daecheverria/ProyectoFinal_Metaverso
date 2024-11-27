using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonScript : MonoBehaviour
{
    public Animator animator;

    public Transform player; // Transform del jugador principal asignado desde el inspector
    public float moveSpeed = 2.5f; // Velocidad de movimiento del enemigo
    public NavMeshAgent IA;

    public GameObject ob1;
    public GameObject ob2;
    public GameObject ob3;

    private void Update()
    {
        //float num = CheckObjects();
        animator.SetBool("isWalking", true);
        IA.speed = moveSpeed; //*num;
        IA.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró en el trigger es el personaje principal
        if (other.CompareTag("Player"))
        {
            // Destruye el objeto
            Debug.Log("Fin del juego: Un enemigo te tocó");
        }
    }

    //private float CheckObjects()
    //{
    //    float num = 1f;
    //    int num1 = 0;
    //    int num2 = 0;
    //    int num3 = 0;
    //    if (ob1 == null)
    //    {
    //        num1 = 1;
    //    }
    //    if (ob2 == null)
    //    {
    //        num2 = 1;
    //    }
    //    if (ob3 == null)
    //    {
    //        num3 = 1;
    //    }
    //    if (num1+num2+num3 >= 3)
    //    {
    //        num = 2f;
    //        Debug.Log("Debería aumentar la velocidad");
    //    }
    //    return num;
    //}
}
