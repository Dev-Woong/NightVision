using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;

    [SerializeField]
    private Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float visualProgress = 0f;
        float speed = 0.7f;

        float[] stopPoints = { 0.21f,0.23f,0.25f,0.38f,0.45f, 0.46f, 0.72f,0.73f,0.74f,0.75f,0.76f,0.9f,0.91f};
        HashSet<float> stopped = new ();

        while (!op.isDone)
        {
            yield return null;

            float realProgress = Mathf.Clamp01(op.progress / 0.9f);
            float targetVisualProgress = (op.progress < 0.9f)? Mathf.Min(realProgress * 0.7f, 0.7f): 1f;

            visualProgress = Mathf.MoveTowards(visualProgress, targetVisualProgress, Time.deltaTime * speed);
            progressBar.fillAmount = visualProgress;

            // 멈칫 타이밍 체크
            foreach (float point in stopPoints)
            {
                if (!stopped.Contains(point) && visualProgress >= point)
                {
                    stopped.Add(point);
                    yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
                    break;
                }
            }

            // 완료 처리
            if (op.progress >= 0.90f && visualProgress >= 0.95f)
            {
                yield return new WaitForSeconds(Random.Range(0.6f, 0.9f));
                BGMManager.Instance.BGMCoroutineProcess(); // 코루틴이면 yield return 필요

                while (progressBar.fillAmount < 0.999f)
                {
                    progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, 1f, Time.deltaTime * speed);
                    yield return null;
                }

                progressBar.fillAmount = 1f; // 보정
                yield return new WaitForSeconds(2f);

                op.allowSceneActivation = true;
                progressBar.fillAmount = 0f;
                yield break;
            }
        }
    }
}