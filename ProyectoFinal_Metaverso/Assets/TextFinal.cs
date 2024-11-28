using UnityEngine;
using UnityEngine.SceneManagement;

public class TextFinal : MonoBehaviour
{
    [SerializeField] DialogosSO dialogos;
    [SerializeField] Dialogue dialogo;
    void Awake(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!dialogos.GetCheckboxValue("Final"))
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
        dialogos.SetCheckboxValue("Final", true);
    }
}