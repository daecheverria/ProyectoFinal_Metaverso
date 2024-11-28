using UnityEngine; 
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MisionesSO", menuName = "Game Data/Misiones Checklist")]
public class MisionesSO : ScriptableObject
{
    [System.Serializable]
    public class Mision
    {
        public string name;
        public string descripcion;
        public bool complete;
    }

    public List<Mision> misiones = new List<Mision>();

    public void SetCheckboxValue(string nombre, bool value)
    {
        foreach (var mision in misiones)
        {
            if (mision.name == nombre)
            {
                mision.complete = value;

                // Verificar si todas las misiones están completas
                if (AreAllMissionsComplete())
                {
                    OnAllMissionsComplete(); // Llama a la función cuando todas están completas
                }
                return;
            }
        }
    }

    public bool GetCheckboxValue(string nombre)
    {
        foreach (var mision in misiones)
        {
            if (mision.name == nombre)
            {
                return mision.complete;
            }
        }
        return false; 
    }

    public void SetAllCheckboxesFalse()
    {
        foreach (var mision in misiones)
        {
            mision.complete = false;
        }
    }

    private bool AreAllMissionsComplete()
    {
        foreach (var mision in misiones)
        {
            if (!mision.complete)
            {
                return false;
            }
        }
        return true;
    }

    private void OnAllMissionsComplete()
    {
        PantallaCarga.Instance.CargarEscena(5);
    }
}
