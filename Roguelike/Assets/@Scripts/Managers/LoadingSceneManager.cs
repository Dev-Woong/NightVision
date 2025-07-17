using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingSceneManager : MonoBehaviour
{
    public TMP_Text lt;
    public TMP_Text tt;
    public TMP_Text nmn;
    public TMP_Text nm;
    public TMP_Text cmn;

    public GameObject cbg;
    public GameObject nbg;
    public GameObject tr0;
    public GameObject tr1;
    public GameObject tr2;



    public static bool onLoadScene=false;

    public string[] lta;
    public string[] tta;
    public string[] nma;

    public static string nextScene;
    public static string curMapName;
    public static string nextMapName;


    [SerializeField]
    private Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
        StartCoroutine(CoLoadingText());
        StartCoroutine(CoTText());
        cbg.SetActive(false);
        nbg.SetActive(false);
        tr0.SetActive(false);
        tr1.SetActive(false);
        tr2.SetActive(false);
    }

    public static void LoadScene(string nextsceneName, MapData nextMapData = null)
    {
        nextScene = nextsceneName;
        SceneManager.LoadScene("LoadingScene");
        if (nextMapData != null)
        {
            curMapName = nextMapData.curmapName;
            nextMapName = nextMapData.nextmapName;
        }
    }
    IEnumerator CoLoadingText()
    {
        while (true)
        {
            lt.text = lta[0];
            yield return new WaitForSeconds(0.5f);
            lt.text = lta[1];
            yield return new WaitForSeconds(0.5f);
            lt.text = lta[2];
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator CoTText()
    {
        while (true)
        {
            int i = Random.Range(0, tta.Length);
            tt.text = tta[i];
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator CoNmText()
    {
        while (true)
        {
            
            nm.text = nma[0];
            yield return new WaitForSeconds(1.8f);
            nm.text = nma[1];
            yield return new WaitForSeconds(1.8f);
            nm.text = nma[2];
            yield return new WaitForSeconds(1.8f);
            nm.text = nma[3];
            yield return new WaitForSeconds(1.8f);
            nm.text = nma[4];
            yield return new WaitForSeconds(1.8f);
            nm.text = nma[5];
            yield return new WaitForSeconds(1.8f);


        }
    }

    IEnumerator LoadScene()
    {
        yield return null;
        if (curMapName != null)
        {
            cmn.text = curMapName;
            nmn.text = nextMapName;
            
            cbg.SetActive(true);
            nbg.SetActive(true);

            tr0.SetActive(true);
            tr1.SetActive(true);
            tr2.SetActive(true);
        }
        else
        {

            StartCoroutine(CoNmText());
        }
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        yield return new WaitForSeconds(0.1f);

        float visualProgress = 0f;
        float speed = 1f;

        float[] stopPoints = { 0.21f,0.45f,0.46f,0.73f,0.74f,0.75f,0.91f};
        HashSet<float> stopped = new ();

        while (!op.isDone)
        {
            yield return null;

            float realProgress = Mathf.Clamp01(op.progress / 0.9f);
            float targetVisualProgress = (op.progress < 0.9f)? Mathf.Min(realProgress * 0.7f, 0.7f): 1f;

            visualProgress = Mathf.MoveTowards(visualProgress, targetVisualProgress, Time.deltaTime * speed);
            progressBar.fillAmount = visualProgress;

            foreach (float point in stopPoints)
            {
                if (!stopped.Contains(point) && visualProgress >= point)
                {
                    stopped.Add(point);
                    yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
                    break;
                }
            }

            if (op.progress >= 0.90f && visualProgress >= 0.95f)
            {
                yield return new WaitForSeconds(Random.Range(0.6f, 0.9f));
                BGMManager.Instance.BGMCoroutineProcess(); 

                while (progressBar.fillAmount < 0.999f)
                {
                    progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, 1f, Time.deltaTime * speed);
                    yield return null;
                }

                progressBar.fillAmount = 1f; // º¸Á¤
                yield return new WaitForSeconds(1.2f);
                op.allowSceneActivation = true;
                
                while (true) 
                { 
                    if (op.allowSceneActivation == true)
                    {
                        
                        onLoadScene = op.allowSceneActivation;
                        cmn.text = "";
                        nmn.text = "";
                        StopCoroutine(CoNmText());
                        
                        //nm.gameObject.SetActive(false);
                    }
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }
}