using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    [Header("SettingPanel")]
    public GameObject settingPanel;
    public GameObject soundPanel;

    [Header("SoundPanel")]
    public Slider MasterSlider;
    public Slider EffectSlider;
    public Slider MusicSlider;

    public GameObject temp;

    private int settingPanelCount;
    private int soundPanelCount;

    private void Awake()
    {
        var value = AudioManager.Instance.AudioVolumeGet();
        MasterSlider.value = value[0];
        EffectSlider.value = value[1];
        MusicSlider.value = value[2];
    }

    public void SetVolume()
    {
        AudioManager.Instance.AudioVolumeSave(MasterSlider.value, EffectSlider.value, MusicSlider.value);
        AudioManager.Instance.AudioVolumeSet();

        // Debug.Log("값 변경");
    }

    public void SettingButton()
    {
        settingPanelCount = ++soundPanelCount % 2;

        if (settingPanelCount == 1)
        {
            settingPanel.transform.localPosition = Vector3.zero;
        }
        else
        {
            settingPanel.transform.localPosition = Vector3.right * 3000;
        }

    }

    // 음량 조절 버튼
    public void soundButton()
    {
        soundPanelCount = ++soundPanelCount % 2;

        if (soundPanelCount == 1)
        {
            soundPanel.transform.localPosition = Vector3.right * 3000;
        }
        else
        {
            soundPanel.transform.position = settingPanel.transform.position;
        }
    }

    // 엔딩 버튼
    public void EndingButton()
    {
        SceneLoader.Instance.ChangeScene("EndingScene");
    }

    // 종료 버튼
    public void OnQuitButton()
    {
        GameManager.Instance.QuitGame();
    }


}
