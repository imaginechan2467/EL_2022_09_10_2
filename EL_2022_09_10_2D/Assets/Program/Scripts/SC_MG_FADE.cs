using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_MG_FADE : MonoBehaviour
{
    private static SC_MG_FADE instance;

    public static SC_MG_FADE Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SC_MG_FADE)FindObjectOfType(typeof(SC_MG_FADE));
                instance.Init();
                if (instance == null)
                {
                    Debug.LogError(typeof(SC_MG_FADE) + "is nothing");
                }
            }

            return instance;
        }
    }

    private enum Fade
    {
        None,
        In,
        Out
    };

    [SerializeField] Image fadeImage;
    Fade fadeState = Fade.None;
    string nextScene;
    float fadeInTime = 0.0f;
    float fadeInTimeMax = 0.0f;
    float fadeOutTime = 0.0f;
    float fadeOutTimeMax = 0.0f;
    Color fadeColor = new Color(0, 0, 0, 0);

    private void Init()
    {
        fadeImage.color = fadeColor;
        DontDestroyOnLoad(this);
        if(instance != this)
        {
            DestroyObject(this);
        }
    }
    private void Awake()
    {
       SC_MG_FADE ins = (SC_MG_FADE)FindObjectOfType(typeof(SC_MG_FADE));
        if (ins != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (fadeState == Fade.Out)
        {
            fadeOutTime -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, 1.0f - (fadeOutTime / fadeOutTimeMax));
            if (fadeOutTime <= 0.0f)
            {
                fadeImage.color = new Color(0, 0, 0, 1);
                LoadScene();
                fadeState = Fade.In;
            }
        }
        else if (fadeState == Fade.In)
        {
            fadeInTime -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, (fadeInTime / fadeInTimeMax));
            if (fadeInTime <= 0.0f)
            {
                fadeImage.color = new Color(0, 0, 0, 0);
                fadeState = Fade.None;
            }
        }
    }

    public void ChangeScene(string next, float inTime, float outTime)
    {
        fadeInTimeMax = fadeInTime = inTime;
        fadeOutTimeMax = fadeOutTime = outTime;
        nextScene = next;
        fadeState = Fade.Out;
    }

    private void LoadScene()
    {
        if(nextScene == "Exit")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
                       UnityEngine.Application.Quit();
#endif
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
