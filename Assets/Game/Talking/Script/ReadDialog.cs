using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ReadDialog : MonoBehaviour
{
    private List<Dictionary<string, object>> _dataDialog;
    private List<GameObject> _options = new List<GameObject>();
    private int _dialogNum, _maximumDialog, _minimumDialog;
    public bool talkingEnd;


    public Action<string, int, string, string> OnOptionSelected;
    public LogDisplay logDisplay;
    public CharacterMotion characterMotion;

    [SerializeField] private GameObject optionButton;
    [SerializeField] private Transform optionsPanel;
    [SerializeField] private CanvasGroup optionsCanvasGroup;
    [SerializeField] private TMP_Text dialogText;

    [SerializeField] private AudioClip clip;

    private void OnEnable()
    {
        OnOptionSelected += ShowOptionResult;
        dialogText.OnPreRenderText += TextEffect;
    }

    private void OnDisable()
    {
        OnOptionSelected -= ShowOptionResult;
        dialogText.OnPreRenderText -= TextEffect;
    }

    private void TextEffect(TMP_TextInfo info)
    {
        AudioManager.Instance.uiSource.PlayOneShot(clip);
    }

    private void Awake()
    {
        _dataDialog = CSVReader.Read("Dialog_" + GameManager.Instance.nowTalkingCharacter);
    }

    private void Start()
    {
        ReadFirst();
    }

    private void ShowOptionResult(string select, int score, string answer, string motion)
    {
        logDisplay.OnAddText("[선택] " + select);

        optionsCanvasGroup.gameObject.SetActive(false);

        foreach (var i in _options)
        {
            Destroy(i);
        }

        var n = GameManager.Instance.Progress[0];
        PlayerPrefs.SetInt("gameProgress", n + score);

        characterMotion.ChangeMotion(motion);

        dialogText.DOKill();
        dialogText.text = string.Empty;
        dialogText.DOText(answer, 8f).SetSpeedBased(true);
        logDisplay.OnAddText(answer);
        talkingEnd = true;
    }

    private bool CheckContent(int index)
    {
        if (!_dataDialog[index]["Content"].Equals(string.Empty)) return true;
        if (string.IsNullOrEmpty(_dataDialog[index]["Option_1"].ToString()))
        {
            // 대화종료
            return true;
        }

        dialogText.DOKill(true);
        optionsCanvasGroup.gameObject.SetActive(true);

        if (!string.IsNullOrEmpty(_dataDialog[index][$"Option_1"].ToString()))
        {
            var cache = Instantiate(optionButton, optionsPanel);
            cache.GetComponentInChildren<TMP_Text>().text = _dataDialog[index][$"Option_1"].ToString();
            var data = cache.GetComponent<OptionData>();
            data.select = _dataDialog[index][$"Option_1"].ToString();
            data.score = Convert.ToInt32(_dataDialog[index][$"Option_Score_1"].ToString());
            data.answer = _dataDialog[index][$"Answer_1"].ToString();
            data.motion = _dataDialog[index][$"Motion_1"].ToString();
            _options.Add(cache);
        }

        if (!string.IsNullOrEmpty(_dataDialog[index][$"Option_2"].ToString()))
        {
            var cache = Instantiate(optionButton, optionsPanel);
            cache.GetComponentInChildren<TMP_Text>().text = _dataDialog[index][$"Option_2"].ToString();
            var data = cache.GetComponent<OptionData>();
            data.select = _dataDialog[index][$"Option_2"].ToString();
            data.score = Convert.ToInt32(_dataDialog[index][$"Option_Score_2"].ToString());
            data.answer = _dataDialog[index][$"Answer_2"].ToString();
            data.motion = _dataDialog[index][$"Motion_2"].ToString();
            _options.Add(cache);
        }
        return false;
    }

    private void ReadFirst()
    {
        var dialogs = new List<int>();

        foreach (var data in _dataDialog)
        {
            if (data["Progress"].ToString().Equals(GameManager.Instance.Progress[1].ToString()))
            {
                dialogs.Add(_dataDialog.IndexOf(data));
            }
        }
        
        var range = Random.Range(0, dialogs.Count);
        _dialogNum = dialogs[range];

        if (GameManager.Instance.nowTalkingCharacter == 6)
            GameManager.Instance.choiOnlyValue = Convert.ToInt32(_dataDialog[_dialogNum]["Char_Num"].ToString());

        if (!CheckContent(_dialogNum)) return;

        dialogText.DOKill();
        dialogText.text = string.Empty;

        dialogText.DOText(_dataDialog[_dialogNum]["Content"].ToString(),
            8f).SetSpeedBased(true);

        logDisplay.OnAddText(_dataDialog[_dialogNum]["Content"].ToString());
    }

    public void ReadNext()
    {
        if (talkingEnd)
        {
            DOTween.KillAll(true);
            var s = GameManager.Instance.Progress[2];
            PlayerPrefs.SetInt("talks", s + 1);
            SceneLoader.Instance.ChangeScene("MainScene");
            return;
        }

        _dialogNum++;

        if (!CheckContent(_dialogNum))
        {
            return;
        }

        dialogText.DOKill();
        dialogText.text = string.Empty;

        dialogText.DOText(_dataDialog[_dialogNum]["Content"].ToString(),
            8f).SetSpeedBased(true);

        logDisplay.OnAddText(_dataDialog[_dialogNum]["Content"].ToString());
    }
}