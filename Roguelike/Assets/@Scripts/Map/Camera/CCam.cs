using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CCam : MonoBehaviour
{
    private CinemachineCamera cineCam;
    private CinemachinePositionComposer cpc;

    void Awake()
    {
        // ī�޶� ������Ʈ ��������
        cineCam = GetComponent<CinemachineCamera>();
        cpc = GetComponent<CinemachinePositionComposer>();
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
            if (SceneManager.sceneCount == 1)
            {
                cpc.TargetOffset = new Vector3(0, -0.65f, 0);
                 
            }
            else if (SceneManager.sceneCount == 2)
            {
                cpc.TargetOffset = new Vector3(0, 4f, 0);
            }
            else if (SceneManager.sceneCount == 3)
            {
                cpc.TargetOffset = new Vector3(0, -0.75f, 0);
            }
            else if (SceneManager.sceneCount == 4)
            {
                cpc.TargetOffset = new Vector3(0, 0.32f,0);
            }
            else if (SceneManager.sceneCount == 5)
            {
                cpc.TargetOffset = new Vector3(0, 4.6f, 0);
            }
                Debug.Log("Cinemachine Tracking Target ���� �Ϸ�");
        }
        
    }

}
