using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NewsControl : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject obj;
    public Image img;
    public Sprite[] sprites;

    private void Start()
    {
        if (GameManager.Instance.Progress[1] < 13)
            img.sprite = sprites[GameManager.Instance.Progress[1]];
    }

    public void Show()
    {
        Time.timeScale = 0;
        canvasGroup.DOKill();

        obj.SetActive(true);

        canvasGroup.DOFade(1, 0.2f).SetUpdate(UpdateType.Normal, true).OnStart(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    public void Close()
    {
        canvasGroup.DOKill();

        canvasGroup.DOFade(0, 0.1f).SetUpdate(UpdateType.Normal, true)
            .OnStart(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = true;
            })
            .OnComplete(() =>
            {
                Time.timeScale = 1;
                canvasGroup.blocksRaycasts = false;
            });
    }
}