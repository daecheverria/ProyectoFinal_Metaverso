using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MisionesSO misiones;
    public EasterEggsSO easterEggs;
    public DialogosSO dialogos;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //hola
    }
    public void SaveData()
    {
        SaveLoadManager.SaveMissions(misiones);
        SaveLoadManager.SaveEasterEggs(easterEggs);
        SaveLoadManager.SaveDialogos(dialogos);
    }

    public void LoadData()
    {
        SaveLoadManager.LoadMissions(misiones);
        SaveLoadManager.LoadEasterEggs(easterEggs);
        SaveLoadManager.LoadDialogos(dialogos);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}