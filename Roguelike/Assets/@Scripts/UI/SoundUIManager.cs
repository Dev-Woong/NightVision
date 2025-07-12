using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundUIManager : MonoBehaviour
{
    [SerializeField] private AudioMixer aMixer;
    [SerializeField] private Slider mSlider;
    [SerializeField] private Slider bSlider;
    [SerializeField] private Slider sSlider;
    [SerializeField] private Slider aSlider;
    void Start()
    {
        if (PlayerPrefs.HasKey("Master"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetBGMVolume();
            SetSFXVolume();
            SetAmbienceVolume();
        }
       
    }
    public void SetMasterVolume()
    {
        float value = mSlider.value;
        aMixer.SetFloat("Master",Mathf.Log10(value)*20);
        PlayerPrefs.SetFloat("Master", value);
    }
    public void SetBGMVolume()
    {
        float value = bSlider.value;
        aMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("BGM", value);
    }
    public void SetSFXVolume()
    {
        float value =sSlider.value;
        aMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFX", value);
    }
    public void SetAmbienceVolume()
    {
        float value = aSlider.value;
        aMixer.SetFloat("Ambience", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Ambience", value);
    }
    private void LoadVolume()
    {
        mSlider.value = PlayerPrefs.GetFloat("Master");
        bSlider.value = PlayerPrefs.GetFloat("BGM");
        sSlider.value = PlayerPrefs.GetFloat("SFX");
        aSlider.value = PlayerPrefs.GetFloat("Ambience");
    }
}
