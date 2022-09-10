using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sound_manager;

    //BGM
    [System.Serializable]
    public class BGM_Data
    {
        public string BGM_Name;
        public AudioClip BGM_Clip;
    }
    [SerializeField]
    private BGM_Data[] BGM_Datas;

    //SE
    [System.Serializable]
    public class SE_Data
    {
        public string SE_Name;
        public AudioClip SE_Clip;
    }
    [SerializeField]
    private SE_Data[] SE_Datas;

    //volume
    public float BGM_Volume;
    public float SE_Volume;

    //AudioSouce
    private AudioSource[] BGM_Source = new AudioSource[2];
    private AudioSource[] SE_Source = new AudioSource[10];

    //Dictionary
    private Dictionary<string, BGM_Data> BGM_Dictionary = new Dictionary<string, BGM_Data>();
    private Dictionary<string, SE_Data> SE_Dictionary = new Dictionary<string, SE_Data>();

    void Awake()
    {
        //singlton
        if(sound_manager == null)
        {
            sound_manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //BGM_AudioSource
        BGM_Source[0] = gameObject.AddComponent<AudioSource>();
        BGM_Source[1] = gameObject.AddComponent<AudioSource>();

        //Set Dictionary(BGM)
        foreach (var bgmData in BGM_Datas)
        {
            BGM_Dictionary.Add(bgmData.BGM_Name, bgmData);
        }

        //SE_AudioSource
        for (var i = 0; i < SE_Source.Length; i++)
        {
            SE_Source[i] = gameObject.AddComponent<AudioSource>();
        }

        //Set Dictionary(SE)
        foreach (var seData in SE_Datas)
        {
            SE_Dictionary.Add(seData.SE_Name, seData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Set Volume
        BGM_Source[0].volume = BGM_Source[1].volume = BGM_Volume;

        if (BGM_Source[0].isPlaying == false)
        {
            BGM_Source[0].Stop();
            BGM_Source[0].clip = null;
        }
        if (BGM_Source[1].isPlaying == false)
        {
            BGM_Source[1].Stop();
            BGM_Source[1].clip = null;
        }

        for (var i = 0; i < SE_Source.Length; i++)
        {
            SE_Source[i].volume = SE_Volume;

            if (SE_Source[i].isPlaying == false)
            {
                SE_Source[i].Stop();
                SE_Source[i].clip = null;
            }
        }
    }

    private AudioSource GetUnusedBGMAudioSource()
    {
        if (BGM_Source[0].isPlaying == false)
        {
            BGM_Source[1].Stop();
            BGM_Source[1].clip = null;
            return BGM_Source[0];
        }

        else if (BGM_Source[1].isPlaying == false)
        {
            BGM_Source[0].Stop();
            BGM_Source[0].clip = null;
            return BGM_Source[1];
        }
        return null;
    }

    private AudioSource GetUnusedSEAudioSource()
    {
        foreach (var seSource in SE_Source)
        {
            if (seSource.isPlaying == false)
            {
                return seSource;
            }
        }
        return null;
    }

    public void PlayBGM(string name)
    {
        if (BGM_Dictionary.TryGetValue(name, out var bgmData))
        {
            //Œ©‚Â‚©‚Á‚½‚çÄ¶
            PlayBGM(bgmData.BGM_Clip);
        }
        else
        {
            Debug.LogWarning($"‚»‚Ì–¼‘O‚Í“o˜^‚³‚ê‚Ä‚¢‚Ü‚¹‚ñF{name}");
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        var audioSource = GetUnusedBGMAudioSource();
        if (audioSource == null) return; //Ä¶‚Å‚«‚Ü‚¹‚ñ‚Å‚µ‚½
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlaySE(string name)
    {
        if (SE_Dictionary.TryGetValue(name, out var seData))
        {
            PlaySE(seData.SE_Clip);
        }
        else
        {
            Debug.LogWarning($"‚»‚Ì–¼‘O‚Í“o˜^‚³‚ê‚Ä‚¢‚Ü‚¹‚ñF{name}");
        }
    }

    public void PlaySE(AudioClip clip)
    {
        var audioSource = GetUnusedSEAudioSource();
        if (audioSource == null) return;
        audioSource.clip = clip;
        audioSource.Play();
    }

    //Stop BGM
    public void StopBGM(string name)
    {
        if (BGM_Dictionary.TryGetValue(name, out var bgmData))
        {
            //Œ©‚Â‚©‚Á‚ÄÄ¶’†‚È‚ç’âŽ~
            if (BGM_Source[0].isPlaying == true && BGM_Source[0].clip == bgmData.BGM_Clip)
            {
                BGM_Source[0].Stop();
                BGM_Source[0].clip = null;
            }

            if (BGM_Source[1].isPlaying == true && BGM_Source[1].clip == bgmData.BGM_Clip)
            {
                BGM_Source[1].Stop();
                BGM_Source[1].clip = null;
            }
        }
    }

    //Stop All BGM
    public void StopAllBGM()
    {
        BGM_Source[0].Stop();
        BGM_Source[0].clip = null;

        BGM_Source[1].Stop();
        BGM_Source[1].clip = null;
    }

    //Stop SE
    public void StopAllSE()
    {
        foreach (AudioSource source in SE_Source)
        {
            source.Stop();
            source.clip = null;
        }
    }
}
