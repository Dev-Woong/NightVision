using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public BGMData[] bData;
    public AudioSource aSource;
    [SerializeField] private Scene curScene;
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        ChangeBGM();
        aSource.Play();
    }
    public void ChangeBGM()
    {
        curScene = SceneManager.GetActiveScene();
        for (int i = 0; i < bData.Length; i++)
        {
            if (bData[i].sceneNum ==curScene.buildIndex)
            {
                aSource.resource = bData[i].BGM;
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
        while (aSource.volume > 0.1f)
        {
            aSource.volume -= 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
        ChangeBGM();
        aSource.Play();
        while (aSource.volume < 0.35f)
        {
            aSource.volume += 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
