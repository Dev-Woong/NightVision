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
    public int a;
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
        aSource.clip = bData[0].BGM;
        aSource.Play();
        a = SceneManager.GetActiveScene().buildIndex;
    }
    public void ChangeBGM()
    {
        
        for (int i = 0; i < bData.Length; i++)
        {
            if (LoadingSceneManager.isDie == false)
            {
                if (bData[i].sceneNum == a + 1)
                {
                    aSource.clip = bData[a + 1].BGM;
                    a++;
                    break;
                }
            }
            else
            {
                aSource.clip = bData[2].BGM;
                a = 2;
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
        yield break;
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
        a = SceneManager.GetActiveScene().buildIndex-1;
        yield break;
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
        yield break;
    }
}
