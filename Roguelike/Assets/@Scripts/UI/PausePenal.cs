using UnityEngine;
using UnityEngine.UI;

public class PausePenal : MonoBehaviour
{

    public GameObject pausepenal;
    public GameObject optionpenal;

    public Button connect;
    public Button option;
    public Button Exit;

    bool on = false;
    
    void Start()
    {
        connect.onClick.AddListener(EnConnect);
        option.onClick.AddListener(EnOption);
        Exit.onClick.AddListener(EnExit);
    }

    void EnConnect()
    {
        pausepenal.SetActive(false);
        optionpenal.SetActive(false);
        Time.timeScale = 1;
    }

    void EnOption()
    {
        optionpenal.SetActive(true);
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
