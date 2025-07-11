using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public AudioClip[] aClip;
    public AudioSource aSource;
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
        aSource.clip = aClip[0];
        aSource.Play();
    }
    public void BGMChange(int SceneNum)
    {
        aSource.resource = aClip[SceneNum];
        aSource.Play();
    }
    IEnumerator ChangeBGM()
    {
        while (aSource.volume > 0.1f)
        {
            aSource.volume -= 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
        aSource.clip = aClip[1];
        aSource.Play();
        while (aSource.volume < 0.35f)
        {
            aSource.volume += 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    void Update()
    {
        if (aSource.clip != aSource.clip)
        {
           BGMChange(SceneManager.sceneCount);
        }
        else
        {
            return;
        }
    }
}
