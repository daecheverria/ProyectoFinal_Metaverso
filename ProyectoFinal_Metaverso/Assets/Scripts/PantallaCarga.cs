using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaCarga : MonoBehaviour
{
    public static PantallaCarga Instance;
    public GameObject pantallaCarga;
    public Image barra;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CargarEscena(int ID)
    {
        StartCoroutine(CargarEscenaAsync(ID));
    }
    private IEnumerator CargarEscenaAsync(int ID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(ID);
        pantallaCarga.SetActive(true);

        while (!operation.isDone)
        {
            float progreso = Mathf.Clamp01(operation.progress / 0.9f);
            barra.fillAmount = progreso;
            yield return null;
        }

        pantallaCarga.SetActive(false);
    }
}
