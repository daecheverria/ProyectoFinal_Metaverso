using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using TMPro;

public class EasterEggsDisplay : MonoBehaviour
{
    [SerializeField] private EasterEggsSO easterEggsSO; 
    [SerializeField] private Transform contentPanel; 
    [SerializeField] private GameObject easterEggItemPrefab; 

    public void PopulateScrollView()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var easterEgg in easterEggsSO.easterEggs)
        {
            GameObject itemInstance = Instantiate(easterEggItemPrefab, contentPanel);
            LocalizeStringEvent[] localizedTexts = itemInstance.GetComponentsInChildren<LocalizeStringEvent>();
            if (localizedTexts.Length >= 2)
            {
                localizedTexts[0].StringReference.SetReference("Easter Eggs", easterEgg.nombrekey);
                localizedTexts[1].StringReference.SetReference("Easter Eggs", easterEgg.completado ? easterEgg.descripcionkey : "???");

                localizedTexts[0].RefreshString();
                localizedTexts[1].RefreshString();
            }

            Image statusImage = itemInstance.GetComponentInChildren<Image>();
            if (statusImage != null)
            {
                statusImage.color = easterEgg.completado ? Color.green : Color.red;
            }
        }
    }
}
