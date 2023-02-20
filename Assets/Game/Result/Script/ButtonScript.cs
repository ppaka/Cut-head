using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject EndingScript;
    public AudioClip HappyEnding;
    public AudioClip BadEnding;
    public void Ending(int i)
    {
        if (i == 0)
        {
            EndingScript.SetActive(true);
        }
        else if (i == 1)
        {
            GameManager.Instance.ResetGame();
            SceneLoader.Instance.ChangeScene("EndingScene");
        }
    }

    public void Continue()
    {
        if (GameManager.Instance.Progress[1] < 12)
            SceneLoader.Instance.ChangeScene("MainScene");
    }

}