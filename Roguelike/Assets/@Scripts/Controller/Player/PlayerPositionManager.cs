using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionManager : MonoBehaviour
{

    private string targetSpawnId;

    void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� ������ ��� SpawnPoint�� ã��
        SpawnPoint[] spawnPoints = GameObject.FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);

        foreach (var point in spawnPoints)
        {
            if (point.spawnId == targetSpawnId)
            {
                transform.position = point.transform.position;
                break;
            }
        }
    }

    public void SetTargetSpawnId(string newId)
    {
        targetSpawnId = newId;
    }
}