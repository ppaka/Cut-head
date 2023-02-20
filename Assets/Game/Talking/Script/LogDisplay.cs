using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogDisplay : MonoBehaviour
{
    public GameObject textPrefab, displayCanvas;
    public CanvasGroup canvasGroup;
    public VerticalLayoutGroup vertical;
    public Transform parent;

    public void OnAddText(string text)
    {
        var cache = Instantiate(textPrefab, parent).GetComponent<TMP_Text>();
        cache.text = text;
        UpdateLayout();
    }

    public void Open()
    {
        canvasGroup.DOFade(1, 0);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        UpdateLayout();
    }
    
    public void Close()
    {
        canvasGroup.DOFade(0, 0);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        UpdateLayout();
    }

    private void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(vertical.GetComponent<RectTransform>());
    
        vertical.CalculateLayoutInputHorizontal();
        vertical.CalculateLayoutInputVertical();
        vertical.SetLayoutHorizontal();
        vertical.SetLayoutVertical();
    }
}
