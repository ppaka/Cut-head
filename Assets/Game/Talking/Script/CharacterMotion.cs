using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMotion : MonoBehaviour
{
    public Image charImage;
    private Sequence _sequence;
    public ReadDialog readDialog;
    public Sprite[] motions;
    private Dictionary<string, Sprite> _dictionary = new Dictionary<string, Sprite>();

    private void Start()
    {
        _dictionary.Add("1", motions[0]); // 체사장
        _dictionary.Add("2", motions[1]);
        _dictionary.Add("3", motions[2]);
        _dictionary.Add("4", motions[3]); // 김부장
        _dictionary.Add("5", motions[4]);
        _dictionary.Add("6", motions[5]);
        _dictionary.Add("7", motions[6]); // 최차장
        _dictionary.Add("8", motions[7]);
        _dictionary.Add("9", motions[8]);
        _dictionary.Add("10", motions[9]); // 결과장
        _dictionary.Add("11", motions[10]);
        _dictionary.Add("12", motions[11]);
        _dictionary.Add("13", motions[12]); // 천대리
        _dictionary.Add("14", motions[13]);
        _dictionary.Add("15", motions[14]);
        _dictionary.Add("17", motions[15]); // 최대리
        _dictionary.Add("18", motions[16]);
        _dictionary.Add("19", motions[17]); // 청소부
        _dictionary.Add("20", motions[18]);
        _dictionary.Add("21", motions[19]);

        if (GameManager.Instance.nowTalkingCharacter == 1)
            charImage.sprite = _dictionary["1"];
        else if (GameManager.Instance.nowTalkingCharacter == 2)
            charImage.sprite = _dictionary["4"];
        else if (GameManager.Instance.nowTalkingCharacter == 3)
            charImage.sprite = _dictionary["7"];
        else if (GameManager.Instance.nowTalkingCharacter == 4)
            charImage.sprite = _dictionary["10"];
        else if (GameManager.Instance.nowTalkingCharacter == 5)
            charImage.sprite = _dictionary["13"];
        else if (GameManager.Instance.nowTalkingCharacter == 6)
        {
            if(GameManager.Instance.choiOnlyValue == 1) 
                charImage.sprite = _dictionary["1"];
            else if (GameManager.Instance.choiOnlyValue == 2)
                charImage.sprite = _dictionary["4"];
            else if (GameManager.Instance.choiOnlyValue == 3)
                charImage.sprite = _dictionary["7"];
            else if (GameManager.Instance.choiOnlyValue == 4)
                charImage.sprite = _dictionary["10"];
            else if (GameManager.Instance.choiOnlyValue == 5)
                charImage.sprite = _dictionary["13"];
            else if (GameManager.Instance.choiOnlyValue == 7)
                charImage.sprite = _dictionary["19"];
        }
        else if (GameManager.Instance.nowTalkingCharacter == 7)
            charImage.sprite = _dictionary["19"];
    }
    
    public void Wiggle()
    {
        if (readDialog.talkingEnd) return;
        _sequence = DOTween.Sequence()
            .Insert(0, charImage.rectTransform.DOScaleX(1.25f, 0.1f))
            .Insert(0, charImage.rectTransform.DOScaleY(0.8f, 0.1f))
            .Insert(0.1f, charImage.rectTransform.DOScaleX(1, 0.1f))
            .Insert(0.1f, charImage.rectTransform.DOScaleY(1, 0.1f));
    }

    public void ChangeMotion(string value)
    {
        Wiggle();
        try{
            charImage.sprite = _dictionary[value];
        }
        catch{
            Debug.Log("이미지 없음");
        }
    }
}