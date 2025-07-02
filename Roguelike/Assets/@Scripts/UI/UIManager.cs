using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

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

    public GameObject pausepenal;
    public GameObject optionpenal;

    bool on = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
