using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

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

    //SE連続再生ループON/OFF用フラグ
    private bool SEPlayOK = false;

    public class _Info
    {
        //クリップの名前
        public string Name;
        //再生済みかどうかのフラグ
        public bool IsDone;
        //再生候補になってからの経過フレーム数
        public int FrameCount;
        //再生する音
        public AudioClip Clip;
    }

    [SerializeField, Range(1, 10)] private int delayFrameCount = 2;
    [SerializeField, Range(0, 32)] private int maxQueuedItemCount;

    //管理中のクリップ
    private readonly Dictionary<string, Queue<_Info>> table = new Dictionary<string, Queue<_Info>>();

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
        DontDestroyOnLoad(gameObject);//シーンをまたぐと壊れるシングルトンor壊れないsingleton 

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

    //SEの複数再生に関してのUpdate
    public void Update()
    {
        if (this.SEPlayOK) 
        {
            foreach (var q in this.table.Values)
            {
                if (q.Count == 0)
                {
                    continue;
                }

                while (true)
                {
                    if (q.Count == 0)
                    {
                        break;
                    }

                    if (q.Peek().IsDone)
                    {
                        var _ = q.Dequeue();
                    }
                    else
                    {
                        break;
                    }
                }

                if (q.Count == 0)
                {
                    continue;
                }
                //未再生でキューの先頭の1件に対して
                var info = q.Peek();
                info.FrameCount++;
                if (info.FrameCount > this.delayFrameCount)
                {
                    Debug.Log("aa");
                    this.seAudioSource.PlayOneShot(info.Clip, SeVolume * Volume);
                    var _ = q.Dequeue();
                }
            }

            if (this.count == 0)
            {
                this.table.Clear();
                this.SEPlayOK = false;
            } 
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

    public void StopBGM()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = null;
    }
    #endregion

    #region SE再生・ストップ
    public void PlaySE(int index)
    {
        var pair = seIndex.FirstOrDefault(c => c.Value == index);
        var SEname = pair.Key;

        this.SEPlayOK = true;
        index = Mathf.Clamp(index, 0, se.Length);

        var info = new _Info() { FrameCount = 0, Clip = se[index] };

        if (!this.table.ContainsKey(SEname))
        {
            this.seAudioSource.PlayOneShot(se[index], SeVolume * Volume);
            info.IsDone = true;//再生済み⇒キューの順番が来たら破棄される。

            var q = new Queue<_Info>();
            q.Enqueue(info);
            this.table[SEname] = q;
        }
        else
        {
            var list = this.table[SEname];//該当SEnameのキューを持ってきて
            if(list.Count < this.maxQueuedItemCount)//max以下のキューの数ならば追加
            {
                this.table[SEname].Enqueue(info);
            }
            else//max超えたキュー登録はしない
            {
                Debug.Log($"効果音の最大登録数を越えました。SE名={SEname}");
            }
        }

        //seAudioSource.PlayOneShot(se[index], SeVolume * Volume);
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

    //有効なSE要素数を取得
    private int count
    {
        get
        {
            int num = 0;
            foreach(var list in this.table.Values)
            {
                num += list.Count;
            }
            return num;
        }
    }
}
