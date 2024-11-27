using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObject : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró en el trigger es el personaje principal
        if (other.CompareTag("Player"))
        {
            // Destruye el objeto
            Destroy(gameObject);
        }
    }
}
