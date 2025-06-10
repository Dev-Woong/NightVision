using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CCam : MonoBehaviour
{
    private CinemachineCamera cineCam;

    void Awake()
    {
        // 카메라 컴포넌트 가져오기
        cineCam = GetComponent<CinemachineCamera>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬 전환 후 실행
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // DontDestroyOnLoad된 플레이어 찾기 (태그 방식 추천)
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogWarning("플레이어를 찾을 수 없습니다.");
            return;
        }

        if (cineCam != null)
        {
            cineCam.Target.TrackingTarget = playerObj.transform;
            Debug.Log("Cinemachine Tracking Target 설정 완료");
        }
    }

}
