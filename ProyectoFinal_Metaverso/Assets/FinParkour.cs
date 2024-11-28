using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinParkour : MonoBehaviour
{
    public MisionesSO misiones;

    void OnDestroy(){
        misiones.SetCheckboxValue("M1N",true);
    }
}
