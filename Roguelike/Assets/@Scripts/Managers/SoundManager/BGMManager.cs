using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public BGMData[] bData;
    public BGMData ShopBGMData;
    public BGMData BossBGMData;
    public AudioSource aSource;
    public AudioMixer master;
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
    public void ChangeBossBGM()
    {
        aSource.clip = BossBGMData.BGM;
    }
    public void EnterBossBattle()
    {
       
            StartCoroutine(nameof(PlayBossBGM));
       
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
        aSource.Stop();
        ChangeShopBGM();
        aSource.Play();
        while (aSource.volume < 0.30f)
        {
            aSource.volume += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator PlayBossBGM()
    {
        while (aSource.volume > 0.05f)
        {
            aSource.volume -= 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
        aSource.Stop();
        ChangeBossBGM();
        aSource.Play();
        while (aSource.volume < 0.30f)
        {
            aSource.volume += 0.05f;
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
        aSource.Stop();
        ChangeBGM();
        aSource.Play();
        while (aSource.volume < 0.3f)
        {
            aSource.volume += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
