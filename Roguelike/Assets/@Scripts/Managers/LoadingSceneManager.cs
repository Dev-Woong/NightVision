using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;

    [SerializeField]
    Image progressBar;

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
        
        float speed = 0.3f;

        while (!op.isDone)
        {
            yield return null;

            float realProgress = Mathf.Clamp01(op.progress / 0.9f);

            float targetVisualProgress = Mathf.Min(realProgress * 0.7f, 0.7f);

            visualProgress = Mathf.MoveTowards(visualProgress, targetVisualProgress, Time.deltaTime * speed);
            progressBar.fillAmount = visualProgress;

            if (op.progress >= 0.9f && visualProgress >= 0.7f)
            {
                float finalFillTarget = 1f;
                while (progressBar.fillAmount < 0.999f)
                {
                    progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, finalFillTarget, Time.deltaTime * speed);
                    yield return null;
                }
                yield return new WaitForSeconds(0.5f);
                op.allowSceneActivation = true;
                progressBar.fillAmount = 0f;    
                yield break;
            }
        }
    }
}