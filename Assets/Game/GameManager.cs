using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (_instance != null) return _instance;
            var obj = FindObjectOfType<GameManager>();
            _instance = obj != null ? obj : Create();
            return _instance;
        }
        private set => _instance = value;
    }

    private static GameManager Create()
    {
        return Instantiate(Resources.Load<GameManager>("GameManager"));
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }

    public int nowTalkingCharacter, choiOnlyValue;
    public int character = 1;

    public int[] Progress
    {
        get
        {
            int[] i = {PlayerPrefs.GetInt("gameProgress", 0), PlayerPrefs.GetInt("seasonProgress", 0), PlayerPrefs.GetInt("talks", 0)};
            return i;
        }
        set
        {
            if (value[0] != 0)
            {
                PlayerPrefs.SetInt("gameProgress", value[0]);
                Progress[0] = value[0];
            }
            if (value[1] != 0)
            {
                PlayerPrefs.SetInt("seasonProgress", value[1]);
                Progress[1] = value[1];
            }
            if (value[2] != 0)
            {
                PlayerPrefs.SetInt("talks", value[2]);
                Progress[2] = value[2];
            }
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.SetInt("gameProgress", 0);      //평판
        PlayerPrefs.SetInt("seasonProgress", 0);    // 계절
        PlayerPrefs.SetInt("talks", 0);
        PlayerPrefs.SetInt("playerOldPositionNum", 0);
        PlayerPrefs.SetInt("maintainNum", 0);
        PlayerPrefs.SetInt("firstTalkWithSajang", 0);
        
        PlayerPrefs.SetInt("Reset", 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}