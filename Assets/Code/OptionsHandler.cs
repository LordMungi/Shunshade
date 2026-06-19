using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider UISlider;
    private void Awake()
    {
        masterSlider.value = SettingsManager.instance.GetMixerValue("MasterVolume");
        musicSlider.value = SettingsManager.instance.GetMixerValue("MusicVolume");
        SFXSlider.value = SettingsManager.instance.GetMixerValue("SFXVolume");
        UISlider.value = SettingsManager.instance.GetMixerValue("UIVolume");
    }

    private void OnEnable()
    {
        masterSlider.onValueChanged.AddListener(SettingsManager.instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SettingsManager.instance.SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SettingsManager.instance.SetSFXVolume);
        UISlider.onValueChanged.AddListener(SettingsManager.instance.SetUIVolume);
    }
    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(SettingsManager.instance.SetMasterVolume);
        musicSlider.onValueChanged.RemoveListener(SettingsManager.instance.SetMusicVolume);
        SFXSlider.onValueChanged.RemoveListener(SettingsManager.instance.SetSFXVolume);
        UISlider.onValueChanged.RemoveListener(SettingsManager.instance.SetUIVolume);
    }
}
