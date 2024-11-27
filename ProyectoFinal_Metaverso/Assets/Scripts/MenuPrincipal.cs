using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] DialogosSO dialogos;
    public void NuevoJuego()
    {   
        dialogos.SetAllCheckboxesFalse();
        SceneManager.LoadScene(1);
    }
}