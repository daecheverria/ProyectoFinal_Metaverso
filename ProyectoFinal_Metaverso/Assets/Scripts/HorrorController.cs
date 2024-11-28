using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorController : MonoBehaviour
{
    bool object1;
    bool object2;
    bool object3;
    public GameObject ob1;
    public GameObject ob2;
    public GameObject ob3;

    public GameObject pumkin1;
    public GameObject pumkin2;
    public GameObject pumkin3;
    public MisionesSO misiones;

    // Start is called before the first frame update
    void Start()
    {
        object1 = false;
        object2 = false;
        object3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckObjects();
    }

    private void CheckObjects()
    {
        if (ob1 == null && !object1)
        {
            object1 = true;
            Debug.Log("Object 1 ha sido destruido.");
            pumkin1.SetActive(true);

        }

        if (ob2 == null && !object2)
        {
            object2 = true;
            Debug.Log("Object 2 ha sido destruido.");
            pumkin2.SetActive(true);
        }

        if (ob3 == null && !object3)
        {
            object3 = true;
            Debug.Log("Object 3 ha sido destruido.");
            pumkin3.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entr� en el trigger es el personaje principal
        if (other.CompareTag("Player"))
        {
            if (object1 && object2 && object3)
            {
                Debug.Log("El nivel termina");
                misiones.SetCheckboxValue("M2N",true);
            }
            else
            {
                Debug.Log("No has obtenido todos los objetos especiales");
            }
        }
    }
}
