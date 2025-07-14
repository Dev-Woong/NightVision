using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public BGMData[] bData;
    public BGMData ShopBGMData;
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
            if (bData[i].sceneNum == curScene.buildIndex)
            {
                aSource.clip = bData[i].BGM;
                break;
            }
        }
    }
    public void ChangeShopBGM()
    {
        aSource.clip = ShopBGMData.BGM;
    }
    public void EnterShopBGM(bool enterShop)
    {
        if (enterShop == true)
        {
            StartCoroutine(nameof(PlayShopBGM));
        }
        else 
        {
            BGMCoroutineProcess();
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BGMCoroutineProcess();
    }
    public void BGMCoroutineProcess()
    {
        StartCoroutine(nameof(PlayBGM));
    }
    IEnumerator PlayShopBGM()
    {
        while (aSource.volume > 0.05f)
        {
            aSource.volume -= 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
        ChangeShopBGM();
        aSource.Play();
        while (aSource.volume < 0.30f)
        {
            aSource.volume += 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator PlayBGM()
    {
        while (aSource.volume > 0.05f)
        {
            aSource.volume -= 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
        ChangeBGM();
        aSource.Play();
        while (aSource.volume < 0.25f)
        {
            aSource.volume += 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
