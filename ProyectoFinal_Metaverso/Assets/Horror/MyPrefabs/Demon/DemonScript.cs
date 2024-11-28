using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DemonScript : MonoBehaviour
{
    public Animator animator;

    public Transform player; // Transform del jugador principal asignado desde el inspector
    public float moveSpeed = 2.5f; // Velocidad de movimiento del enemigo
    public NavMeshAgent IA;


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
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
