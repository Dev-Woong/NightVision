using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject panel;

    private void Awake()
    {
        if (instance == null) // instance가 시스템 상에 존재하고 있지 않을 경우
        {
            instance = this; // 자신을 instance로 넣어준다
            DontDestroyOnLoad(gameObject); // onLoad(씬이 로드 되었을 때) 자신을 파괴하지 않고 유지
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (instance != this) // instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
            {
                Destroy(this.gameObject); // 둘 이상 존재하면 안되는 객체로 방금 Awake된 자신을 삭제
            }
        }
    }

    private void Update()
    {
        
    }
}
