using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using TMPro;

public class MisionesDisplay : MonoBehaviour
{
    [SerializeField] private MisionesSO misionesSO; 
    [SerializeField] private Transform contentPanel; 
    [SerializeField] private GameObject itemPrefab; 

    public void PopulateScrollView()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var mision in misionesSO.misiones)
        {
            GameObject itemInstance = Instantiate(itemPrefab, contentPanel);
            LocalizeStringEvent[] localizedTexts = itemInstance.GetComponentsInChildren<LocalizeStringEvent>();
            if (localizedTexts.Length >= 2)
            {
                localizedTexts[0].StringReference.SetReference("Misiones", mision.name);
                localizedTexts[1].StringReference.SetReference("Misiones", mision.descripcion);

                localizedTexts[0].RefreshString();
                localizedTexts[1].RefreshString();
            }

            Image statusImage = itemInstance.GetComponentInChildren<Image>();
            if (statusImage != null)
            {
                statusImage.color = mision.complete ? Color.green : Color.red;
            }
        }
    }
}
