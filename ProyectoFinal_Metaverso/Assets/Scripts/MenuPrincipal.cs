using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] DialogosSO dialogos;
    [SerializeField] GameObject menu;
    public void NuevoJuego()
    {   
        //GameManager.instance.SaveData();
        //GameManager.instance.LoadData();
        dialogos.SetAllCheckboxesFalse();
        GameManager.instance.easterEggs.SetAllCheckboxesFalse();
        GameManager.instance.misiones.SetAllCheckboxesFalse();
        menu.SetActive(false);
        PantallaCarga.Instance.CargarEscena(1);
    }
    public void CargarJuego()
    {   
        GameManager.instance.LoadData();
        menu.SetActive(false);
        PantallaCarga.Instance.CargarEscena(1);
    }
}