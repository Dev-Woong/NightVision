using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public BGMData[] bData;
    public AudioSource aSource;
    public Scene curScene;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
    }
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        ChangeBGM();
        aSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void ChangeBGM()
    {
        curScene = SceneManager.GetActiveScene();
        for (int i = 0; i < bData.Length; i++)
        {
            if (bData[i].sceneNum ==curScene.buildIndex)
            {
                aSource.clip = bData[i].BGM;
                break;
            }
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(nameof(PlayBGM));
    }
    
    IEnumerator PlayBGM()
    {
        while (aSource.volume > 0.05f)
        {
            aSource.volume -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        ChangeBGM();
        aSource.Play();
        while (aSource.volume < 0.25f)
        {
            aSource.volume += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
