using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CambiarEscena : MonoBehaviour
{
    [SerializeField] private GameObject textoPrefab;
    private GameObject textoInstancia;
    [SerializeField] private int idObjetivo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (textoInstancia == null)
            {
                textoInstancia = Instantiate(textoPrefab, transform.position, Quaternion.identity);

                StartCoroutine(CheckPlayerExistence());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && textoInstancia != null)
        {
            Destroy(textoInstancia);
        }
    }
    private IEnumerator CheckPlayerExistence()
    {
        while (textoInstancia != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Destroy(textoInstancia);
            }
            yield return new WaitForSeconds(1f); // Verificar cada segundo
        }
    }


    public void CambioEscena(InputAction.CallbackContext context)
    {
        if (context.performed && textoInstancia != null)
        {
            PantallaCarga.Instance.CargarEscena(idObjetivo);
        }
    }
}
