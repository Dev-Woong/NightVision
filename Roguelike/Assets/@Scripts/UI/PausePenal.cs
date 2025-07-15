using UnityEngine;
using UnityEngine.UI;

public class PausePenal : MonoBehaviour
{

    public GameObject pausepanel;
    public GameObject settingPanel;
    public GameObject KeyPanel;

    public Button connect;
    public Button option;
    public Button key;
    public Button Exit;
    
    void Start()
    {
        connect.onClick.AddListener(EnConnect);
        option.onClick.AddListener(EnOption);
        key.onClick.AddListener(EnKey);
        Exit.onClick.AddListener(EnExit);

    }

    void EnConnect()
    {
        pausepanel.SetActive(false);
        settingPanel.SetActive(false);
        Time.timeScale = 1;
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
