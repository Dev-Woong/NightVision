using UnityEngine;

public class MenuController : MonoBehaviour
{
    public CustomButton[] buttons;
    private int selectedIndex = 0;

    void Start()
    {
        UpdateSelection();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + buttons.Length) % buttons.Length;
            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % buttons.Length;
            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[selectedIndex].Press(); // enter 키로 실행
        }
    }

    void UpdateSelection()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == selectedIndex)
                buttons[i].Select();
            else
                buttons[i].Deselect();
        }
    }
}
