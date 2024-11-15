using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{
    public Camera mainCamera; // Referencia a la c�mara principal
    public float visibilityThreshold = 10f; // Umbral para determinar si el enemigo est� en la vista de la c�mara
    public Animator animator;

    void Update()
    {
        CheckIfVisible();
    }

    void CheckIfVisible()
    {
        // Obtener la posici�n del enemigo en la vista de la c�mara
        Vector3 enemyScreenPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Verificar si la posici�n est� dentro de los l�mites de la pantalla
        bool isVisible = enemyScreenPosition.x > visibilityThreshold &&
                         enemyScreenPosition.x < 1 - visibilityThreshold &&
                         enemyScreenPosition.y > visibilityThreshold &&
                         enemyScreenPosition.y < 1 - visibilityThreshold &&
                         enemyScreenPosition.z > 0;

        if (isVisible)
        {
            Debug.Log("El enemigo est� en la vista de la c�mara principal.");
            animator.SetBool("isWalking", false);
        }
        else
        {
            Debug.Log("El enemigo no est� en la vista de la c�mara principal.");
            animator.SetBool("isWalking", true);
        }
    }
}
