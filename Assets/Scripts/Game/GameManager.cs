using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float baseVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float musicVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float ambienceVolume = 1f;

    private static GameManager manager;
    public static GameManager Manager => manager;

    public float BaseVolume
    {
        get { return baseVolume; }
        set { baseVolume = value; }
    }

    public float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = value; }
    }

    public float SFXVolume
    {
        get { return sfxVolume; }
        set { sfxVolume = value; }
    }

    public float AmbienceVolume
    {
        get { return ambienceVolume; }
        set { ambienceVolume = value; }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode _)
    {
        if (scene.name == "ForestDay")
        {
            ObjectiveManager.CreateObjective("photo1", "Photograph the BROKEN GATE", 1);
        }

        if (scene.name == "ForestNight")
        {
            ObjectiveManager.CreateObjective("exp4", "Explore the FIREWATCH TOWER", 1);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
