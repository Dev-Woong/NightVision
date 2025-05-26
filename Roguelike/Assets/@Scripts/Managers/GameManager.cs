using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject panel;

    private void Awake()
    {
        if (instance == null) // instance�� �ý��� �� �����ϰ� ���� ���� ���
        {
            instance = this; // �ڽ��� instance�� �־��ش�
            DontDestroyOnLoad(gameObject); // onLoad(���� �ε� �Ǿ��� ��) �ڽ��� �ı����� �ʰ� ����
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (instance != this) // instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
            {
                Destroy(this.gameObject); // �� �̻� �����ϸ� �ȵǴ� ��ü�� ��� Awake�� �ڽ��� ����
            }
        }
    }

    private void Update()
    {
        
    }
}
