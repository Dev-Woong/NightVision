using UnityEngine;
using UnityEngine.UI;

public class PausePenal : MonoBehaviour
{

    public GameObject pausepanel;
    public GameObject settingPanel;
    public GameObject KeyPanel;

    public Button resume;
    public Button option;
    public Button key;
    public Button Exit;
    
    void Start()
    {
        resume.onClick.AddListener(EnResume);
        option.onClick.AddListener(EnOption);
        key.onClick.AddListener(EnKey);
        Exit.onClick.AddListener(EnExit);
    }

    void EnResume()
    {
        Time.timeScale = 1;
        LoadingController.onPause = false;
        UIManager.Instance.on = false;
        pausepanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    void EnOption()
    {
        settingPanel.SetActive(true);
    }

    void EnKey()
    {
        KeyPanel.SetActive(true);
    }

    void EnExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
