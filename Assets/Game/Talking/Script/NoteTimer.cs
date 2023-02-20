using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NoteTimer : MonoBehaviour
{
    public GameObject noteObject;
    public TMP_Text text;
    private float _time = 10;
    public bool isStart;
    public AudioClip bgm;

    private void Update()
    {
        if (isStart == false || noteObject.gameObject == null)
        {
            AudioManager.Instance.audioSource.Pause();
            return;
        }
        if (!AudioManager.Instance.audioSource.isPlaying)
        {
            AudioManager.Instance.audioSource.clip = bgm;
            AudioManager.Instance.audioSource.Play();
        }
        
        _time -= Time.deltaTime;
        text.text = Mathf.CeilToInt(_time).ToString();
        if (_time <= 0)
        {
            Destroy(noteObject);
        }
    }
}