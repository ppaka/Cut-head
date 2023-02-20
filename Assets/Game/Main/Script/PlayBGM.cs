using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public AudioClip bgm;
    
    private void Start()
    {
        AudioManager.Instance.audioSource.clip = bgm;
        AudioManager.Instance.audioSource.loop = true;
        AudioManager.Instance.audioSource.Play();
    }
}
