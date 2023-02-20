using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public enum SceneType
    {
        Talk,
        Main
    }

    public SceneType sceneType = SceneType.Talk;
    [Header("Note")] public GameObject scrollView;
    public GameObject noteContent;
    public Transform noteUpPos, noteDownPos;
    [Header("Button")] public GameObject button;
    public Vector3 buttonUpPos;
    public Vector3 buttonDownPos;
    [Header("Sprite")] public Sprite defaultSprite;
    public Sprite pressedSprite;

    private int _count;
    private Vector3 _settingPos;
    private List<Dictionary<string, object>> _memoData;

    public TMP_Text noteText, noteCharacterText;

    public NoteTimer noteTimer;

    private void Start()
    {
        _memoData = CSVReader.Read("Memo");
        UpdateMemo();

        _settingPos = noteContent.transform.GetChild(0).transform.position;

        for (var i = 0; i < noteContent.transform.childCount; i++)
            noteContent.transform.GetChild(i).GetComponent<NoteButton>().num = i;

        NoteButtonSpriteSet(0);
    }

    public void UpdateMemo()
    {
        foreach (var data in _memoData)
        {
            if (!data["Game_Progress"].ToString().Equals(GameManager.Instance.Progress[1].ToString())) continue;
            if (data["Char_Num"].ToString().Equals(GameManager.Instance.character.ToString()))
            {
                if (data["Char_Num"].ToString().Equals(GameManager.Instance.character.ToString()))
                {
                    noteText.text = data["Text"].ToString();
                    if (data["Char_Num"].ToString().Equals("1"))
                        noteCharacterText.text = "체사장";
                    else if (data["Char_Num"].ToString().Equals("2"))
                        noteCharacterText.text = "김부장";
                    else if (data["Char_Num"].ToString().Equals("3"))
                        noteCharacterText.text = "최차장";
                    else if (data["Char_Num"].ToString().Equals("4"))
                        noteCharacterText.text = "결과장";
                    else if (data["Char_Num"].ToString().Equals("5"))
                        noteCharacterText.text = "천대리";
                    else if (data["Char_Num"].ToString().Equals("6"))
                        noteCharacterText.text = "최대리";
                    else if (data["Char_Num"].ToString().Equals("7"))
                        noteCharacterText.text = "청소부";
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (sceneType == SceneType.Talk)
            {
                noteTimer.isStart = false;
            }

            NotePosDown();
            NoteButtonPosGatherSet();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (sceneType == SceneType.Talk)
            {
                noteTimer.isStart = true;
            }

            NotePosUp();
            NoteButtonPosSpreadSet();
        }
    }

    public void NoteUpDown()
    {
        _count = ++_count % 2;
        //Debug.Log(_count);

        if (_count == 1)
        {
            if (sceneType == SceneType.Talk)
            {
                noteTimer.isStart = true;
            }

            button.GetComponent<RectTransform>().anchoredPosition = buttonUpPos;
            NoteUp();
        }
        else
        {
            if (sceneType == SceneType.Talk)
            {
                noteTimer.isStart = false;
            }

            button.GetComponent<RectTransform>().anchoredPosition = buttonDownPos;
            NoteDown();
        }
    }


    private void NoteUp()
    {
        NotePosUp();
        NoteButtonPosSpreadSet();
    }

    private void NoteDown()
    {
        NotePosDown();
        NoteButtonPosGatherSet();
    }

    public void NoteButtonSpriteSet(int num)
    {
        for (var i = 0; i < noteContent.transform.childCount; i++)
            noteContent.transform.GetChild(i).GetComponent<Image>().sprite = defaultSprite;

        noteContent.transform.GetChild(num).GetComponent<Image>().sprite = pressedSprite;
    }

    private void NoteButtonPosSpreadSet()
    {
        DOTween.Kill(noteContent.GetComponent<HorizontalLayoutGroup>().spacing);

        DOTween.To(() => noteContent.GetComponent<HorizontalLayoutGroup>().spacing,
            value => noteContent.GetComponent<HorizontalLayoutGroup>().spacing = value,
            0, 0.5f).SetEase(Ease.OutExpo);
    }

    private void NoteButtonPosGatherSet()
    {
        DOTween.Kill(noteContent.GetComponent<HorizontalLayoutGroup>().spacing);

        DOTween.To(() => noteContent.GetComponent<HorizontalLayoutGroup>().spacing,
            value => noteContent.GetComponent<HorizontalLayoutGroup>().spacing = value,
            -366, 0.5f).SetEase(Ease.OutExpo);
    }

    private void NotePosUp()
    {
        scrollView.transform.DOMoveY(noteUpPos.transform.position.y, 0.5f).SetEase(Ease.OutExpo);
    }

    private void NotePosDown()
    {
        scrollView.transform.DOMoveY(noteDownPos.transform.position.y, 0.5f).SetEase(Ease.OutExpo);
    }
}