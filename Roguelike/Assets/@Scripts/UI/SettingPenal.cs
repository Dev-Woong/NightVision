
using UnityEngine;
using UnityEngine.UI;

public class SettingPenal : MonoBehaviour
{
   
    public Button button;
    public GameObject Penal;

    
    void Start()
    {
        button.onClick.AddListener(EnButton);
    }

    
    void EnButton()
    {
        Penal.SetActive(false);
    }
}
