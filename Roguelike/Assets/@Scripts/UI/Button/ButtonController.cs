using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    
    public Button startgame;
    public Button setting;
    public Button quit;

    public GameObject SettingPanel;

    

    

    void Start()
    {
        startgame.onClick.AddListener(StartGameScene);
        setting.onClick.AddListener(OnSetting);
        quit.onClick.AddListener(QuitButton);
        SettingPanel.SetActive(false);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SettingPanel.activeSelf)
        {
            SettingPanel.SetActive(false);
        }
            
    }
    void StartGameScene()
    {
        LoadingSceneManager.LoadScene("Game");
    }

    void OnSetting()
    {
        SettingPanel.SetActive(true);
    }

    void QuitButton()
    {
        #if     UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    
}
