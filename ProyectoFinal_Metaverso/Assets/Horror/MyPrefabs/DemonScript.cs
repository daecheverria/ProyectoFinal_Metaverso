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

    public bool IsCameraSeeingEnemy()
    {
        if (assignedCamera == null)
        {
            Debug.LogError("La cámara no está asignada en el inspector.");
            return false;
        }

        Vector3 viewportPosition = assignedCamera.WorldToViewportPoint(transform.position);
        bool isInViewport = viewportPosition.z > 0 &&
                            viewportPosition.x > 0 && viewportPosition.x < 1 &&
                            viewportPosition.y > 0 && viewportPosition.y < 1;
        if (!isInViewport){
            return false;
        }

        // Lanza un Raycast para verificar si algo bloquea la vista al enemigo
        Vector3 directionToEnemy = (transform.position - assignedCamera.transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(assignedCamera.transform.position, directionToEnemy, out hit))
        {
            return hit.transform == transform;
        }
        return false;
    }

    //public void HandleEnemyMovement()
    //{
    //    if (IsCameraSeeingEnemy())
    //    {
    //        // La cámara está viendo al enemigo, no se mueve
    //        Debug.Log("La cámara está viendo al enemigo, se detiene.");
    //        isWalking = false;
    //        return;
    //    }

    //    if (player == null)
    //    {
    //        Debug.LogError("El jugador no está asignado en el inspector.");
    //        return;
    //    }

    //    // La cámara no está viendo al enemigo, se mueve hacia el jugador
    //    Debug.Log("La cámara no está viendo al enemigo, se mueve hacia el jugador.");
    //    isWalking = true;
    //    Vector3 direction = (player.position - transform.position).normalized;
    //    transform.position += direction * moveSpeed * Time.deltaTime;
    //}

    private void Update()
    {
        if (!IsCameraSeeingEnemy())
        {
            IA.speed = moveSpeed;
            IA.SetDestination(player.position);
            animator.SetBool("isWalking", true);
            Debug.Log("Debería estar caminando");
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        

        //HandleEnemyMovement();
    }
}
