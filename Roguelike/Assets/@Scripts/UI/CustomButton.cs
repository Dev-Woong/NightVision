using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    public enum ButtonActionType { StartGame, OpenOptions, ExitGame }
    public ButtonActionType actionType;

    public Animator animator;
    public TMP_Text text;
    public Button unityUIButton;

    private void Start()
    {
        // ���콺 Ŭ�� �ÿ��� ������ Press() ���� ����
        if (unityUIButton != null)
        {
            unityUIButton.onClick.AddListener(Press);
        }
    }

    public void Select()
    {
        animator.SetTrigger("Press");
        animator.SetBool("IsSelected", true);
        text.color = Color.black;
        
    }

    public void Deselect()
    {
        animator.SetBool("IsSelected", false);
        text.color = Color.white;
    }

    public void Press()
    {
        
        switch (actionType)
        {
            case ButtonActionType.StartGame:
                SceneManager.LoadScene("Game");
                break;

            case ButtonActionType.OpenOptions:
                Debug.Log("�ɼ� ����");
                // optionsPanel.SetActive(true);
                break;

            case ButtonActionType.ExitGame:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                break;
        }
    }
}
