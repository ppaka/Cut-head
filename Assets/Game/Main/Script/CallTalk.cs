using UnityEngine;

public class CallTalk : MonoBehaviour
{
    public void Call()
    {
        while (true)
        {
            int num = Random.Range(1, 8);
            if (GameManager.Instance.Progress[1] == 9 && num == 1)
                continue;
            if (GameManager.Instance.Progress[1] == 6 && num == 7)
                continue;
            if (GameManager.Instance.Progress[1] > 8 && num == 2)
                continue;
            if (GameManager.Instance.Progress[1] > 7 && num == 3)
                continue;
            if (GameManager.Instance.Progress[1] > 8 && num == 4)
                continue;
            if (GameManager.Instance.Progress[1] > 8 && num == 5)
                continue;
            if (GameManager.Instance.Progress[1] > 10 && num == 6)
                continue;
            if (GameManager.Instance.Progress[1] > 11 && num == 7)
                continue;
            if (num == 1 && PlayerPrefs.GetInt("firstTalkWithSajang", 0) != 0 && GameManager.Instance.Progress[1] == 0)
                continue;
            if (num == 1 && PlayerPrefs.GetInt("firstTalkWithSajang", 0) == 0 &&
                GameManager.Instance.Progress[1] == 0)
                PlayerPrefs.SetInt("firstTalkWithSajang", 1);
            GameManager.Instance.nowTalkingCharacter = num;

            break;
        }

        SceneLoader.Instance.ChangeScene("TalkingScene");
    }
}