using UnityEngine;

public class EndingButton : MonoBehaviour
{

    public void exitButton()
    {
        if (GameManager.Instance.Progress[1] < 12 && PlayerPrefs.GetInt("Reset", 0) == 0)
            SceneLoader.Instance.ChangeScene("MainScene");
        else
        {
            GameManager.Instance.ResetGame();
            PlayerPrefs.SetInt("Reset", 0);
            SceneLoader.Instance.ChangeScene("TitleScene");
        }
    }
}
