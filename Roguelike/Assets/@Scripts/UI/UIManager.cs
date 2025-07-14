using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private UIManager Instance;
    public GameObject pausepenal;
    public GameObject optionpenal;

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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& inGame == true)
        {
            on = !on;
            pausepenal.SetActive(on);
            if (on == true)
            {
                Time.timeScale = 0;
            }
            if (on == false)
            {
                Time.timeScale = 1;
                optionpenal.SetActive(false);
            }
        }
    }


}
