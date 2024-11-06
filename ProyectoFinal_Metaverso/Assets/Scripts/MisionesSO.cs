using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MisionesSO", menuName = "Game Data/Misiones Checklist")]
public class MisionesSO : ScriptableObject
{
    [System.Serializable]
    public class Mision
    {
        public string nombre;
        public bool completado;
    }
    public List<Mision> misiones = new List<Mision>();

    public void SetCheckboxValue(string nombre, bool value)
    {
        foreach (var mision in misiones)
        {
            if (mision.nombre == nombre)
            {
                mision.completado = value;
                return;
            }
        }
    }

    public bool GetCheckboxValue(string nombre)
    {
        foreach (var mision in misiones)
        {
            if (mision.nombre == nombre)
            {
                return mision.completado;
            }
        }
        return false; 
    }
}
