using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{
    public Camera mainCamera; // Referencia a la cámara principal
    public float visibilityThreshold = 10f; // Umbral para determinar si el enemigo está en la vista de la cámara
    public Animator animator;

    void Update()
    {
        CheckIfVisible();
    }

    void CheckIfVisible()
    {
        // Obtener la posición del enemigo en la vista de la cámara
        Vector3 enemyScreenPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Verificar si la posición está dentro de los límites de la pantalla
        bool isVisible = enemyScreenPosition.x > visibilityThreshold &&
                         enemyScreenPosition.x < 1 - visibilityThreshold &&
                         enemyScreenPosition.y > visibilityThreshold &&
                         enemyScreenPosition.y < 1 - visibilityThreshold &&
                         enemyScreenPosition.z > 0;

        if (isVisible)
        {
            Debug.Log("El enemigo está en la vista de la cámara principal.");
            animator.SetBool("isWalking", false);
        }
        else
        {
            Debug.Log("El enemigo no está en la vista de la cámara principal.");
            animator.SetBool("isWalking", true);
        }
    }
}
