using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    public Button button;
    public GameObject Penal;

    PausePenal pp = new PausePenal();

    void Start()
    {
        button.onClick.AddListener(EnButton);
    }

    
    void EnButton()
    {
        Penal.SetActive(false);
       
    }
}
