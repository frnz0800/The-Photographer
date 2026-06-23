using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    private static LoadingManager manager;
    public static LoadingManager Manager => manager;

    private string sceneName;

    public void StartLoading(string scene)
    {
        sceneName = scene;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        SceneManager.LoadScene("LoadingScreen");

        yield return null;

        if (progressBar == null)
        {
            progressBar = FindAnyObjectByType<Slider>();
        }

        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName);

        while (!loadScene.isDone)
        {
            float progress = Mathf.Clamp01(loadScene.progress / .9f);

            progressBar.value = progress;
        }

        yield return null;
    }

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
