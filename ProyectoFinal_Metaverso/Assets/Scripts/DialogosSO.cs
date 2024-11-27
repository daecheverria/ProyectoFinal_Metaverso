using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogosSO", menuName = "Dialogos/DialogosSO")]
public class DialogosSO : ScriptableObject
{
    [System.Serializable]
    public class Dialogo
    {
        public string dialogoID; 
        public bool reproducido;
    }
    public List<Dialogo> dialogos = new List<Dialogo>();
    public void SetCheckboxValue(string ID, bool value)
    {
        foreach (var dialogo in dialogos)
        {
            if (dialogo.dialogoID == ID)
            {
                dialogo.reproducido = value;
                return;
            }
        }
    }
    public void SetAllCheckboxesFalse()
{
    foreach (var dialogo in dialogos)
    {
        dialogo.reproducido = false;
    }
}


    public bool GetCheckboxValue(string ID)
    {
        foreach (var dialogo in dialogos)
        {
            if (dialogo.dialogoID == ID)
            {
                return dialogo.reproducido;
            }
        }
        return false; 
    }
}
