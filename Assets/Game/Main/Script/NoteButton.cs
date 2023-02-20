using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteButton : MonoBehaviour
{
    public Sprite changeSprite;

    public Image information;
    public int num;

    private delegate void NoteButtonSpriteSet(int num);

    private NoteManager _noteManager;
    private NoteButtonSpriteSet _noteButtonSpriteSet;

    private void Start()
    {
        _noteManager = GameObject.Find("NoteManager").GetComponent<NoteManager>();
        _noteButtonSpriteSet = _noteManager.NoteButtonSpriteSet;
    }

    public void ButtonDown()
    {
        information.sprite = changeSprite;
        _noteButtonSpriteSet(num);
    }

    public void SelectMemo(int character)
    {
        GameManager.Instance.character = character;
        _noteManager.UpdateMemo();
    }
}
