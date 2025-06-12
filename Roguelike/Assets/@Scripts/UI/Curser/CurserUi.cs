using UnityEngine;

public class CurserUi : MonoBehaviour
{
    public RectTransform customCursorUI;

    void Start()
    {
        Cursor.visible = false; // 기본 커서 숨기기

        customCursorUI = GameObject.Find("CustomCurser").GetComponent<RectTransform>();
        Debug.Log(customCursorUI);
    }

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            customCursorUI.parent as RectTransform,
            Input.mousePosition,
            null,
            out pos);
        customCursorUI.anchoredPosition = pos;
    }
}
