using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemController : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    static bool pausado;
    private GameObject menuPausaInstancia;
    public void Pausar(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pausado = !pausado;
            if (!pausado)
            {
                Time.timeScale = 0;
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
    }
}
