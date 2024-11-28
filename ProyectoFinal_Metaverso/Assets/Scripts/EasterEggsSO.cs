using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EasterEggsSO", menuName = "Game Data/Easter Eggs Checklist")]
public class EasterEggsSO : ScriptableObject
{
    [System.Serializable]
    public class EasterEgg
    {
        public string nombre;
        public string nombrekey;
        public string descripcionkey;
        public bool completado;
    }
    public List<EasterEgg> easterEggs = new List<EasterEgg>();

    public void SetCheckboxValue(string nombre, bool value)
    {
        foreach (var easterEgg in easterEggs)
        {
            if (easterEgg.nombre == nombre)
            {
                easterEgg.completado = value;
                return;
            }
        }
    }

    public bool GetCheckboxValue(string nombre)
    {
        foreach (var easterEgg in easterEggs)
        {
            if (easterEgg.nombre == nombre)
            {
                return easterEgg.completado;
            }
        }
        return false; 
    }
    public EasterEgg GetEasterEggByName(string nombre)
    {
        return easterEggs.Find(ee => ee.nombre == nombre);
    }
    public void SetAllCheckboxesFalse()
{
    foreach (var easterEgg in easterEggs)
    {
        easterEgg.completado = false;
    }
}
}
