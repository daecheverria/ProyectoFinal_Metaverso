using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonScript : MonoBehaviour
{
    public Camera assignedCamera; // Cámara asignada desde el inspector
    public float detectionAngle = 30f; // Ángulo de visión de la cámara
    public Animator animator;

    public Transform player; // Transform del jugador principal asignado desde el inspector
    public float moveSpeed = 2f; // Velocidad de movimiento del enemigo
    public NavMeshAgent IA;
    private bool isWalking;

    //public bool IsCameraSeeingEnemy()
    //{
    //    if (assignedCamera == null)
    //    {
    //        Debug.LogError("La cámara no está asignada en el inspector.");
    //        return false;
    //    }

    //    Vector3 viewportPosition = assignedCamera.WorldToViewportPoint(transform.position);
    //    bool isInViewport = viewportPosition.z > 0 &&
    //                        viewportPosition.x > 0 && viewportPosition.x < 1 &&
    //                        viewportPosition.y > 0 && viewportPosition.y < 1;
    //    if (!isInViewport){
    //        return false;
    //    }

    //    // Lanza un Raycast para verificar si algo bloquea la vista al enemigo
    //    Vector3 directionToEnemy = (transform.position - assignedCamera.transform.position).normalized;
    //    RaycastHit hit;

    //    if (Physics.Raycast(assignedCamera.transform.position, directionToEnemy, out hit))
    //    {
    //        return hit.transform == transform;
    //    }
    //    return false;
    //}

    private void Update()
    {

        animator.SetBool("isWalking", true);
        IA.speed = moveSpeed;
        IA.SetDestination(player.position);
        Debug.Log("Debería estar caminando");

        //if (!IsCameraSeeingEnemy())
        //{
        //    animator.SetBool("isWalking", true);
        //    IA.speed = moveSpeed;
        //    IA.SetDestination(player.position);
        //    Debug.Log("Debería estar caminando");
        //}
        //else
        //{
        //    animator.SetBool("isWalking", false);
        //}
    }
}
