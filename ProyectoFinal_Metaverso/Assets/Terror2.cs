using UnityEngine;
using UnityEngine.SceneManagement;

public class Terror2 : MonoBehaviour
{
    [SerializeField] DialogosSO dialogos;
    [SerializeField] Dialogue dialogo;
    void Awake(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!dialogos.GetCheckboxValue("terror"))
        {
            if (dialogo != null)
            {
                dialogo.localizationController.OnLocalizationReady += OnLocalizationReady;
            }
        }
    }
    private void OnLocalizationReady()
    {
        dialogo.SetupDialogue();
        dialogo.StartDialogue(0, false);
        dialogos.SetCheckboxValue("terror", true);
    }
}