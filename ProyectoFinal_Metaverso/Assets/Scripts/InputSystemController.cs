using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using PolyStang;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputSystemController : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    static bool pausado;
    private GameObject menuPausaInstancia;
    public InstanciaPersonajeSO personajeSO;
    public InstanciaCarroSO carroSO;
    public bool dentroCarro;
    public float distanciaMinima;
    public GameObject carroCanvas;
    void Awake()
    {
        dentroCarro = false;
    }
    public void Pausar(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (context.performed)
            {
                Pausa();
            }
        }
    }
    public void Pausa(){
        pausado = !pausado;
                if (!pausado)
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    menuPausaInstancia = Instantiate(menuPausa);
                }
                else
                {
                    Time.timeScale = 1;
                    if (menuPausaInstancia != null)
                    {
                        Destroy(menuPausaInstancia);
                    }
                }
    }
    public void CambiarCarro(InputAction.CallbackContext context)
    {
        if (context.performed && personajeSO.instancia != null && carroSO.instancia != null)
        {
            float distancia = Vector3.Distance(personajeSO.instancia.transform.position, carroSO.instancia.transform.position);
            if (distancia <= distanciaMinima && !dentroCarro)
            {
                EntrarCarro();
            }
            else if (dentroCarro)
            {
                SalirCarro();
            }
        }
    }

    void EntrarCarro()
    {
        dentroCarro = true;
        personajeSO.instancia.SetActive(false);
        carroSO.instancia.GetComponent<PlayerInput>().enabled = true;
        carroSO.instancia.GetComponent<CarSounds>().enabled = true;
        carroSO.instancia.GetComponent<AudioSource>().enabled = true;
        carroCanvas = carroSO.instancia.transform.GetChild(0).gameObject;
        carroCanvas.SetActive(true);

    }
    void SalirCarro()
    {
        dentroCarro = false;
        Vector3 nuevaPosicion = carroSO.instancia.transform.position + (carroSO.instancia.transform.right * -2.0f);
        personajeSO.instancia.transform.position = nuevaPosicion;
        personajeSO.instancia.SetActive(true);
        CharacterController controller = personajeSO.instancia.GetComponent<CharacterController>();
        controller.enabled = false;
        personajeSO.instancia.transform.position = nuevaPosicion;
        controller.enabled = true;
        carroSO.instancia.GetComponent<PlayerInput>().enabled = false;
        carroSO.instancia.GetComponent<CarSounds>().enabled = false;
        carroSO.instancia.GetComponent<AudioSource>().enabled = false;
        carroCanvas = carroSO.instancia.transform.GetChild(0).gameObject;
        carroCanvas.SetActive(false);
    }
}
