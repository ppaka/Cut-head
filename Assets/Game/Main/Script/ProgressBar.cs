using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image seasonImg, progressImg;
    public TMP_Text seasonTypeText, seasonText, progressText;

    private void Start()
    {
        UpdateImage();
    }

    public void UpdateImage()
    {
        var i = GameManager.Instance.Progress[2]/10f;
        // 반년이 지난게 13번째가 됐으면 게임 강제 끝
        if (i == 1)
        {
            PlayerPrefs.SetInt("seasonProgress", PlayerPrefs.GetInt("seasonProgress", 0) + 1);
            PlayerPrefs.SetInt("talks", 0);
            SceneLoader.Instance.ChangeScene("ResultScene");
        }

        seasonImg.fillAmount = GameManager.Instance.Progress[2] / 10f;
        progressImg.fillAmount = GameManager.Instance.Progress[0] / 210f;

        if (GameManager.Instance.Progress[1] % 2 == 0 || GameManager.Instance.Progress[1] == 0)
            seasonTypeText.text = "여름";
        else
            seasonTypeText.text = "겨울";
        seasonText.text = GameManager.Instance.Progress[2] + "/10";
        progressText.text = GameManager.Instance.Progress[0] + "/210";
    }
}