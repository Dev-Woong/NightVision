
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject pausepanel;
    public GameObject optionpanel;
    public GameObject keypanel;
    public GameObject dieImage;

    bool on = false;
    bool inGame = false;
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
    private void Start()
    {
        SceneManager.sceneLoaded += CheckScene;
    }
    private void CheckScene(Scene scene, LoadSceneMode mode)
    {
        Scene curScene = SceneManager.GetActiveScene();
        int sceneNum = curScene.buildIndex;
        if (sceneNum != 0)
            inGame = true;
        else 
            inGame = false; 
    }
    public void PlayerDiePanel()
    {
        dieImage.SetActive(true);
        dieImage.GetComponent<DiePanel>().DieAnim();
    }
    
    void Update()
    {
        if (LoadingController.onInputBlocker != true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && inGame == true)
            {
                on = !on;
                pausepanel.SetActive(on);
                if (on == true)
                {
                    Time.timeScale = 0;
                }
                if (on == false)
                {
                    Time.timeScale = 1;
                    optionpanel.SetActive(false);
                    keypanel.SetActive(false);
                }
            } 
        }
    }


}
