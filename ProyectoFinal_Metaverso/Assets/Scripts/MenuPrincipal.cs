using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] DialogosSO dialogos;
     [SerializeField] GameObject menu;
    public void NuevoJuego()
    {   
        dialogos.SetAllCheckboxesFalse();
        menu.SetActive(false);
        PantallaCarga.Instance.CargarEscena(1);
    }
}