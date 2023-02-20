using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject EndingCanvas;

    public AudioClip HappyEnding;
    public AudioClip BadEnding;
    
    public void Start()
    {
        if (EndingNum() != 99)
        {
            EndingCanvas.SetActive(true);

            AudioManager.Instance.audioSource.clip = audioClip();
            AudioManager.Instance.transform.Find("Background").GetComponent<AudioSource>().Play();
            
            var image = EndingCanvas.transform.GetChild(0).gameObject.GetComponent<Image>();
        
            image.sprite = Resources.Load<Sprite>("Ending/" + EndingNum());
            
            PlayerPrefs.SetInt("AchievedEnding" + EndingNum(), 1);
        }
        else
        {
            var temp = string.Format("벗어난 값 | Calculate : {0} | maintainNum : {1}", PlayerPosition.Calculate(),
                PlayerPrefs.GetInt("maintainNum"));
            Debug.Log(temp);
        }
    }

    public int EndingNum()
    {
        if (PlayerPrefs.HasKey("maintainNum") == false)
        {
            PlayerPrefs.SetInt("maintainNum", 0);
        }
        switch (PlayerPosition.Calculate())
        {
            case 0:
                if (PlayerPrefs.GetInt("maintainNum") == 0)
                {
                    return 1;
                }
                else if (PlayerPrefs.GetInt("maintainNum") == 1)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            case 1:
                if (PlayerPrefs.GetInt("maintainNum") == 0)
                {
                    return 4;
                }
                else if (PlayerPrefs.GetInt("maintainNum") == 1)
                {
                    return 5;
                }
                else
                {
                    return 6;
                }
            case 2:
                if (PlayerPrefs.GetInt("maintainNum") == 0)
                {
                    return 7;
                }
                else if (PlayerPrefs.GetInt("maintainNum") == 1)
                {
                    return 8;
                }
                else
                {
                    return 9;
                }
            case 3:
                if (PlayerPrefs.GetInt("maintainNum") == 0)
                {
                    return 10;
                }
                else if (PlayerPrefs.GetInt("maintainNum") == 1)
                {
                    return 11;
                }
                else
                {
                    return 12;
                }
            case 4:
                if (PlayerPrefs.GetInt("maintainNum") == 0)
                {
                    return 13;
                }
                else if (PlayerPrefs.GetInt("maintainNum") == 1)
                {
                    return 14;
                }
                else
                {
                    return 15;
                }
            case 5:
                if (PlayerPrefs.GetInt("maintainNum") == 0)
                {
                    return 16;
                }
                else if (PlayerPrefs.GetInt("maintainNum") == 1)
                {
                    return 17;
                }
                else
                {
                    return 17;
                }
            case 6:
                if (PlayerPrefs.GetInt("maintainNum") == 0)
                {
                    return 19;
                }
                else if (PlayerPrefs.GetInt("maintainNum") == 1)
                {
                    return 19;
                }
                else
                {
                    return 19;
                }
            
            default: return 99;
        }
    }

    public AudioClip audioClip()
    {
        switch (EndingNum())
        {
            case 1: return BadEnding;
            case 2: return BadEnding;
            case 3: return BadEnding;
            case 4: return HappyEnding;
            case 5: return HappyEnding;
            case 6: return BadEnding;
            case 7: return BadEnding;
            case 8: return BadEnding;
            case 9: return BadEnding;
            case 10: return HappyEnding;
            case 11: return HappyEnding;
            case 12: return BadEnding;
            case 13: return HappyEnding;
            case 14: return HappyEnding;
            case 15: return BadEnding;
            case 16: return HappyEnding;
            case 17: return HappyEnding;
            case 18: return HappyEnding;
            case 19: return HappyEnding;
            
            default: return null;
            
        }
    }
}
