using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CCam : MonoBehaviour
{
    private CinemachineCamera cineCam;

    void Awake()
    {
        // ī�޶� ������Ʈ ��������
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

    // �� ��ȯ �� ����
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // DontDestroyOnLoad�� �÷��̾� ã�� (�±� ��� ��õ)
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogWarning("�÷��̾ ã�� �� �����ϴ�.");
            return;
        }

        if (cineCam != null)
        {
            cineCam.Target.TrackingTarget = playerObj.transform;
            Debug.Log("Cinemachine Tracking Target ���� �Ϸ�");
        }
    }

}
