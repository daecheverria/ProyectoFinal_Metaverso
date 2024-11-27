using UnityEngine;
using UnityEngine.SceneManagement;

public class casaProta : MonoBehaviour
{
    [SerializeField] DialogosSO dialogos;
    [SerializeField] Dialogue dialogo;
    void Awake(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!dialogos.GetCheckboxValue("intro"))
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
        dialogos.SetCheckboxValue("intro", true);
    }
}