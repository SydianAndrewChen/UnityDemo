using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : BasePanel
{
    [SerializeField]
    private Text overallVolume;
    [SerializeField]
    private Text musicVolume;
    [SerializeField]
    private Text soundVolume;

    const string OverallVolumeSliderName = "OverallVolumeSlider";
    const string MusicVolumeSliderName = "MusicVolumeSlider";
    const string SoundSliderName = "SoundVolumeSlider";

    const string OverallVolumeName = "OverallVolume";
    const string MusicVolumeName = "MusicVolume";
    const string SoundVolumeName = "SoundVolume";

    const string CloseButtonName = "CloseButton";
    const string QuitButtonName = "QuitButton";
    const string ContinueButtonName = "ContinueButton";
    // Start is called before the first frame update
    void Start()
    {
        overallVolume = GetControl<Text>(OverallVolumeName);
        musicVolume = GetControl<Text>(MusicVolumeName);
        soundVolume = GetControl<Text>(SoundVolumeName);
        EventCenter.GetInstance().AddEventListener<KeyCode>("Key_Escape_Pressed_Down", (key) =>
        {
            gameObject.SetActive(true);
        });
        InputMgr.GetInstance().StartOrEndCheck(true);
        gameObject.SetActive(false);
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
        switch (btnName) 
        {
            case CloseButtonName:
                gameObject.SetActive(false);
                break;
            case QuitButtonName:
                QuitGame();
                break;
            case ContinueButtonName:
                gameObject.SetActive(false);
                break;
            default:
                Debug.Log("Wrong btnName, please check your code");
                break;
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
