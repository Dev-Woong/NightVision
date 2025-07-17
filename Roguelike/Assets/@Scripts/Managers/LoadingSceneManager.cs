using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingSceneManager : MonoBehaviour
{
    public TMP_Text lt;

    public static bool onLoadScene;

    public string[] lta;

    public static string nextScene;
     
    [SerializeField]
    private Image progressBar;

    

    private void Start()
    {
        StartCoroutine(LoadScene());
        StartCoroutine(CoLoadingText());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
        
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
    IEnumerator LoadScene()
    {
        yield return null;
        onLoadScene = false;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

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
                yield return new WaitForSeconds(0.1f);
                onLoadScene = op.allowSceneActivation;
                yield break;
            }
        }
    }
}