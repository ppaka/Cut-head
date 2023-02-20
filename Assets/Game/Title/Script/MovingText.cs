using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MovingText : MonoBehaviour
{
    public Image titleImg, dayImg;
    public RectTransform rect, endTweenRect;
    private Sequence _seq;
    private float startPos, endPos;

    private void Start()
    {
        startPos = rect.localPosition.y;
        endPos = endTweenRect.localPosition.y;
        
        _seq = DOTween.Sequence()
            .Insert(0, rect.DOLocalMoveY(endPos, 1f).SetEase(Ease.InOutQuart))
            .Insert(1f, rect.DOLocalMoveY(startPos, 1f).SetEase(Ease.InOutQuart))
            .SetLoops(-1, LoopType.Restart).Pause();

        titleImg.DOFade(1, 1).OnComplete(() =>
        {
            rect.gameObject.SetActive(true);
            _seq.Play();
        });
    }

    private bool _isChanging;

    public void StopTween()
    {
        if (_isChanging) return;
        _isChanging = true;
        DOTween.KillAll(false);
        rect.gameObject.SetActive(false);
        titleImg.DOFade(0, 0.5f).OnComplete(() =>
        {
            dayImg.DOFade(1, 1).OnComplete(() => { StartCoroutine(nameof(LoadScene)); });
        });
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        SceneLoader.Instance.ChangeScene("MainScene");
    }
}