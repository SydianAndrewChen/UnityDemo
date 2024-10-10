using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartGamePanel : BasePanel
{
    [SerializeField]
    private GameObject Settings;
    [SerializeField]
    private GameObject Login;

    [SerializeField]
    private Text overallVolume;
    [SerializeField]
    private Text musicVolume;
    [SerializeField]
    private Text soundVolume;

    const string GameSceneName = "Main";
    const string SettingPanelName = "SettingPanel";
    const string LoginPanelName = "LoginPanel";

    const string OverallVolumeSliderName = "OverallVolumeSlider";
    const string MusicVolumeSliderName = "MusicVolumeSlider";
    const string SoundSliderName = "SoundVolumeSlider";

    const string OverallVolumeName = "OverallVolume";
    const string MusicVolumeName = "MusicVolume";
    const string SoundVolumeName = "SoundVolume";
    void Start()
    {
        Settings = GetControl<Image>(SettingPanelName).gameObject;
        Login = GetControl<Image>(LoginPanelName).gameObject;
        Settings.SetActive(false);
        Login.SetActive(false);

        overallVolume = GetControl<Text>(OverallVolumeName);
        musicVolume = GetControl<Text>(MusicVolumeName);
        soundVolume = GetControl<Text>(SoundVolumeName);

    }

    protected override void OnValueChanged(string sliderName, float value)
    {
        switch (sliderName)
        {
            case OverallVolumeSliderName:
                overallVolume.text = (Mathf.Floor(value * 100)).ToString();
                AudioMgr.GetInstance().SetOverallVolume(value);
                break;
            case MusicVolumeSliderName:
                musicVolume.text = (Mathf.Floor(value * 100)).ToString();
                AudioMgr.GetInstance().SetBGMVolume(value);
                break;
            case SoundSliderName:
                soundVolume.text = (Mathf.Floor(value * 100)).ToString();
                AudioMgr.GetInstance().SetSoundVolume(value);
                break;
            default:
                Debug.LogError("Wrong Slider name, check your code");
                break;
        }
    }

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName) 
        {
            case "Start":
                //GetControl<Image>(SettingPanelName).gameObject.SetActive(false);
                //Debug.Log("Success")
                GetControl<Image>(SettingPanelName).gameObject.SetActive(false);
                GetControl<Image>(LoginPanelName).gameObject.SetActive(true);
                break;
            case "Quit":
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
                break;
            case "Setting":
                GetControl<Image>(SettingPanelName).gameObject.SetActive(true);
                GetControl<Image>(LoginPanelName).gameObject.SetActive(false);
                break;
            case "Enter":
                SceneMgr.GetInstance().LoadSceneAsync(GameSceneName);
                UIMgr.GetInstance().HidePanel(gameObject.name);
                break;
            default:
                Debug.LogError("Invalid Btn Name, check your code");
                break;
        }

    }

    


}
