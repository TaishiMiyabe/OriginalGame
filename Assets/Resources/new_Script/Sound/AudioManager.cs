using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField, Range(0, 1), Tooltip("マスタ音量")]
    float volume = 1;
    [SerializeField, Range(0, 1), Tooltip("BGM音量")]
    float bgmVolume = 1;
    [SerializeField, Range(0, 1), Tooltip("SE音量")]
    float seVolume = 1;

    AudioClip[] bgm;
    AudioClip[] se;

    Dictionary<string, int> bgmIndex = new Dictionary<string, int>();
    Dictionary<string, int> seIndex = new Dictionary<string, int>();

    AudioSource bgmAudioSource;
    AudioSource seAudioSource;

    public float Volume
    {
        set
        {
            volume = Mathf.Clamp01(value);
            bgmAudioSource.volume = bgmVolume * volume;
            seAudioSource.volume = seVolume * volume;
        }
        get
        {
            return volume;
        }
    }

    public float BgmVolume
    {
        set
        {
            bgmVolume = Mathf.Clamp01(value);
            bgmAudioSource.volume = bgmVolume * volume;
        }
        get
        {
            return bgmVolume;
        }
    }

    public float SeVolume
    {
        set
        {
            seVolume = Mathf.Clamp01(value);
            seAudioSource.volume = seVolume * volume;
        }
        get
        {
            return seVolume;
        }
    }

    public void Awake()
    {
        //2つ目のインスタンスなら破棄(singletonの特徴)
        if(this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        //シーンが遷移しても破棄しない
        DontDestroyOnLoad(gameObject);

        //AudioSourceの自動アタッチ
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        seAudioSource = gameObject.AddComponent<AudioSource>();

        //bgm, se の一括読み込み
        bgm = Resources.LoadAll<AudioClip>("Audio/BGM");
        se = Resources.LoadAll<AudioClip>("Audio/SE");
        for(int i = 0; i < bgm.Length; i++)
        {
            bgmIndex.Add(bgm[i].name, i);
        }
        for(int i = 0; i < se.Length; i++)
        {
            seIndex.Add(se[i].name, i);
        }
    }
    #region ファイル名からインデックス番号を検索する
    public int GetBgmIndex(string name)
    {
        if (bgmIndex.ContainsKey(name))
        {
            return bgmIndex[name];
        }
        else
        {
            Debug.LogError("指定された名前のBGMファイルが不在");
            return 0;
        }
    }

    public int GetSeIndex(string name)
    {
        if (seIndex.ContainsKey(name))
        {
            return seIndex[name];
        }
        else
        {
            Debug.LogError("指定された名前のSEファイルが不在");
            return 0;
        }
    }
    #endregion
    #region BGM再生・ストップ
    public void PlayBGM(int index)
    {
        index = Mathf.Clamp(index, 0, bgm.Length);//indexの値をbgm[]要素内に制限する。

        bgmAudioSource.clip = bgm[index];
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = BgmVolume * Volume;
        bgmAudioSource.Play();
    }

    public void PlayBGMByName(string name)
    {
        PlayBGM(GetBgmIndex(name));
    }

    public void PlayBGM_spatial(int index)
    {
        PlayBGM(index);
        bgmAudioSource.spatialize = true;
        bgmAudioSource.spatialBlend = 1.0f;
    }

    public void PlayBGMByName_spatial(string name)
    {
        PlayBGM_spatial(GetBgmIndex(name));
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = null;
    }
    #endregion

    #region SE再生・ストップ
    public void PlaySE(int index)
    {
        index = Mathf.Clamp(index, 0, se.Length);

        seAudioSource.PlayOneShot(se[index], SeVolume * Volume);
    }

    public void PlaySEByName(string name)
    {
        PlaySE(GetSeIndex(name));
    }

    public void StopSE()
    {
        seAudioSource.Stop();
        seAudioSource.clip = null;
    }
    #endregion
}
