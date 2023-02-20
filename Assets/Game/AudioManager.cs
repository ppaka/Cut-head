using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioTouch;

    private static AudioManager _instance;
    public AudioSource audioSource, uiSource;
    private Touch touch;

    public static AudioManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (_instance != null) return _instance;
            var obj = FindObjectOfType<AudioManager>();
            _instance = obj != null ? obj : Create();
            return _instance;
        }
    }

    private static AudioManager Create()
    {
        return Instantiate(Resources.Load<AudioManager>("AudioManager"));
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

        DontDestroyOnLoad(gameObject);
    }


    public float[] AudioVolumeGet()
    {
        /*Debug.Log(PlayerPrefs.GetFloat("MasterValue"));
        Debug.Log(PlayerPrefs.GetFloat("EffectValue"));
        Debug.Log(PlayerPrefs.GetFloat("MusicValue"));*/
        
        float[] value = {
            PlayerPrefs.GetFloat("MasterValue", 1), PlayerPrefs.GetFloat("EffectValue", 1),
            PlayerPrefs.GetFloat("MusicValue", 1)
        };

        return value;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                {
                    PlayAudio("Touch");
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlayAudio("Touch");
        }
    }

    public void AudioVolumeSave(float masterValue, float effectValue, float musicValue)
    {
        PlayerPrefs.SetFloat("MasterValue", masterValue);
        PlayerPrefs.SetFloat("EffectValue", effectValue);
        PlayerPrefs.SetFloat("MusicValue", musicValue);

        // Debug.Log("저장");
    }

    public void AudioVolumeSet()
    {
        float masterValue = PlayerPrefs.GetFloat("MasterValue");

        AudioSource temp = Instance.transform.GetChild(0).GetComponent<AudioSource>(); // touch
        temp.volume = masterValue * PlayerPrefs.GetFloat("EffectValue");

        temp = Instance.transform.GetChild(1).GetComponent<AudioSource>(); // background
        temp.volume = masterValue * PlayerPrefs.GetFloat("MusicValue");
    }

    public void PlayAudio(string name, string path = null)
    {
        AudioSource temp = transform.Find(name).GetComponent<AudioSource>();

        if (path != null)
        {
            temp.clip = Resources.Load<AudioClip>("Audio/" + path);
        }

        temp.GetComponent<AudioSource>().Play();
    }
}