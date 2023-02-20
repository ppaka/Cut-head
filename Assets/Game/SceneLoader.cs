using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;

    public CanvasGroup fadeImg;
    private float fadeDuration = 0.5f;
    private float fadeValue = 1;
    public GameObject loading;
    public Image proBar;

    public static SceneLoader Instance
    {
        get
        {
            if (_instance != null) return _instance;
            var obj = FindObjectOfType<SceneLoader>();
            _instance = obj != null ? obj : Create();
            return _instance;
        }
        private set => _instance = value;
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private static SceneLoader Create()
    {
        var sceneLoaderPrefab = Resources.Load<SceneLoader>("SceneLoader");
        return Instantiate(sceneLoaderPrefab);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeImg.DOFade(0, fadeDuration)
            .SetAutoKill(true)
            .OnStart(() =>
            {
                proBar.fillAmount = 0;
                fadeImg.alpha = 1;
                // loading.SetActive(false);
            })
            .OnComplete(() => { fadeImg.blocksRaycasts = false; });
    }

    public void ChangeScene(string sceneName) // 외부에서 전환할 씬 이름 받기
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        fadeImg.DOFade(fadeValue, fadeDuration)
            .OnStart(() =>
            {
                proBar.fillAmount = 0;
                fadeImg.blocksRaycasts = true;
                AudioManager.Instance.audioSource.Stop();
                AudioManager.Instance.audioSource.loop = false;
            })
            .OnComplete(() =>
            {
                DOTween.KillAll(true);
                StartCoroutine(LoadScene(sceneName)); // 씬 로드 코루틴 실행
            });
    }

    private IEnumerator LoadScene(string sceneName)
    {
        //loading.SetActive(true);
        var async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!async.isDone)
        {
            proBar.fillAmount = async.progress * 100;
            yield return null;
        }
    }
}