using UnityEngine;
using UnityEngine.UI;

public class BrightController : MonoBehaviour
{
    public Slider slider;
    public Image image;

    private void Start()
    {
       
        slider.onValueChanged.AddListener(SetBrightness);
        SetBrightness(slider.value);
        
    }

    void SetBrightness(float value)
    {
        if(image != null)
        {
            Color color = image.color;
            color.a = 1f - value;
            image.color = color;
        }
    }
}
