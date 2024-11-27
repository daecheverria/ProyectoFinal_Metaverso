using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public class EasterEgg : MonoBehaviour
{
    [SerializeField] EasterEggsSO easterEggs;
    [SerializeField] string nombre;
    [SerializeField] GameObject easterEggcanvas;
    private int displayTime = 3;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var easterEgg = easterEggs.GetEasterEggByName(nombre);
            if (easterEgg != null && !easterEgg.completado)
            {
                easterEgg.completado = true;
                ShowCanvas(easterEgg.nombrekey, easterEgg.descripcionkey);
            }
        }
    }

    public void ShowCanvas(string tituloKey, string descripcionKey)
    {
        GameObject canvasInstance = Instantiate(easterEggcanvas, transform);

        Transform panelTransform = canvasInstance.transform.Find("Panel");
        if (panelTransform == null)
        {
            Debug.LogError("El Panel no se encontró en el Canvas prefab.");
            Destroy(canvasInstance);
            return;
        }

        LocalizeStringEvent[] localizedTexts = panelTransform.GetComponentsInChildren<LocalizeStringEvent>();

        if (localizedTexts.Length >= 2)
        {
            localizedTexts[0].StringReference.SetReference("Easter Eggs", tituloKey);
            localizedTexts[1].StringReference.SetReference("Easter Eggs", descripcionKey);

            localizedTexts[0].RefreshString();
            localizedTexts[1].RefreshString();
        }
        else
        {
            Debug.LogError("No se encontraron suficientes LocalizeStringEvent en los hijos del Panel.");
            Destroy(canvasInstance);
            return;
        }

        StartCoroutine(HideCanvasAfterTime(canvasInstance));
    }

    private IEnumerator HideCanvasAfterTime(GameObject canvasInstance)
    {
        yield return new WaitForSeconds(displayTime);
        Destroy(canvasInstance);
    }
}
