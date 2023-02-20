using System;
using UnityEngine;
using DG.Tweening;

public class SetText : MonoBehaviour
{
    [Header("Progress")]
    public TMPro.TMP_Text progressText;

    [Header("Details")]
    public TMPro.TMP_Text reputation;
    public TMPro.TMP_Text position;
    public TMPro.TMP_Text maintain;
    public float setDelayTime;

    [Header("Result")]
    public TMPro.TMP_Text result;

    [Header("Button")]
    public GameObject ContinueButton;
    public GameObject EndingButton;

    [Header("EndingCanvas")] public GameObject EndingCanvas;

    private int reputationNum;
    private string oldPositionString;
    private int maintainNum;
    private string resultString;
    void Start()
    {
        Init();

        Progress();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DOTween.CompleteAll();
        if (Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
                DOTween.CompleteAll();
        }
    }

    private void Init()
    {
        progressText.text = string.Empty;
        reputation.text = string.Empty;
        position.text = string.Empty;
        maintain.text = string.Empty;
        result.text = string.Empty;

        reputationNum = GameManager.Instance.Progress[0];

        // 직급 유지
        int playerOldPositionNum = PlayerPrefs.GetInt("playerOldPositionNum", 0);



        if (GameManager.Instance.Progress[0] > 210){
            EndingCanvas.SetActive(true);
            GetComponent<ButtonScript>().EndingScript.SetActive(true);
            GameManager.Instance.ResetGame();
        }

        if (PlayerPosition.Calculate() <= playerOldPositionNum)
        {
            // 직급 유지를 늘려준다
            PlayerPrefs.SetInt("maintainNum", PlayerPrefs.GetInt("maintainNum", 0) + 1);
            oldPositionString = getPlayerPosition(PlayerPrefs.GetInt("maintainNum"));
            resultString = "유지";
        }
        else if (PlayerPosition.Calculate() > playerOldPositionNum)
        {
            oldPositionString = getPlayerPosition(PlayerPrefs.GetInt("maintainNum"));
            PlayerPrefs.SetInt("playerOldPositionNum", PlayerPosition.Calculate());
            resultString = "승진";
        }

        maintainNum = PlayerPrefs.GetInt("maintainNum");

        if (maintainNum > 3)
        {
            PlayerPrefs.SetInt("maintainNum", 0);
            PlayerPrefs.SetInt("AchievedEnding" + 3, 1);
            GameManager.Instance.ResetGame();
            EndingCanvas.SetActive(true);
            DOTween.KillAll(false);
        }
    }

    private void Progress()
    {
        //string strSeason = GameManager.Instance.Progress[1] % 2 == 0 ? "여름" : "겨울";
        var strSeason = string.Empty;

        if (GameManager.Instance.Progress[1] % 2 == 0 || GameManager.Instance.Progress[1] == 0)
            strSeason = "겨울";
        else
            strSeason = "여름";
        float strYear = (float)GameManager.Instance.Progress[1] * 0.5f;
        progressText.DOKill();
        progressText.text = String.Empty;
        progressText.DOText(strSeason + " " + string.Format("{0:N1}년", strYear) + " " + "정산", 1).OnComplete((() => Details()));
    }

    private void Details()
    {
        reputation.DOText("평판 :" + " " + reputationNum, 1f);
        position.DOText("직급 :" + " " + oldPositionString, 1f).SetDelay(setDelayTime).OnComplete(
        () => maintain.DOText("직급 유지 :" + " " + maintainNum, 1f).OnComplete((() => Result()))
        );
    }

    private void Result()
    {
        result.DOText("결과 :" + " " + getPlayerPosition(PlayerPosition.Calculate()) + "(" + resultString + ")", 1f).OnComplete((() =>
        {
            if (GameManager.Instance.Progress[1] < 12)
                ContinueButton.SetActive(true);

            EndingButton.SetActive(true);
        }));
    }

    private string getPlayerPosition(int num)
    {
        switch (PlayerPosition.Calculate())
        {
            case 0:
                return "인턴";
            case 1:
                return "정규직";
            case 2:
                return "대리";
            case 3:
                return "과장";
            case 4:
                return "차장";
            case 5:
                return "부장";
            case 6:
                return "부사장";

            default: return "error";
        }

    }

}