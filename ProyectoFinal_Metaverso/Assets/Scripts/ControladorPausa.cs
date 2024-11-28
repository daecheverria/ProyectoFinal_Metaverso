using UnityEngine;

public class ControladorPausa : MonoBehaviour
{
    [SerializeField] private GameObject scroll;
    [SerializeField] private GameObject boton;
    [SerializeField] private EasterEggsDisplay easterEggsDisplay;
    [SerializeField] private MisionesDisplay misionesDisplay;


    public void ShowEasterEggs()
    {
        scroll.SetActive(true);
        easterEggsDisplay.PopulateScrollView();
        boton.SetActive(true);
    }
    public void ShowMisiones()
    {
        scroll.SetActive(true);
        misionesDisplay.PopulateScrollView();
        boton.SetActive(true);
    }

    public void Hide()
    {
        scroll.SetActive(false);
        boton.SetActive(false);
    }
}