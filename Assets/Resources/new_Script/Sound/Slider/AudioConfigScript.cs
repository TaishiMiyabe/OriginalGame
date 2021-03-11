using UnityEngine;
using UnityEngine.UI;

public class AudioConfigScript : MonoBehaviour
{
    [SerializeField] Slider m_masterSlider = default;
    [SerializeField] Slider m_sfxSlider = default;
    [SerializeField] Slider m_bgmSlider = default;
    AudioManager audio = default;
    
    void Start()
    {
        Initialize();
    }
    
    void Initialize()
    {
        audio = AudioManager.Instance;
        if (audio)
        {
            m_masterSlider.value = audio.Volume;
            m_sfxSlider.value = audio.SeVolume;
            m_bgmSlider.value = audio.BgmVolume;
        }
        else
        {
            Debug.LogError("AudioManager がアタッチされたオブジェクトがシーンに存在しない");
        }
    }
    
    /// <summary>
    /// Slider の Value が変わった時に呼び出す
    /// </summary>
    /// <param name="sender">関数を呼び出した元の Slider</param>
    public void ChangeVolume(Slider sender)
    {
        bool errFlg = false;
        
        if (sender)
        {
            if (sender.Equals(m_masterSlider)) audio.Volume = sender.value;
            else if (sender.Equals(m_sfxSlider)) audio.SeVolume = sender.value;
            else if (sender.Equals(m_bgmSlider)) audio.BgmVolume = sender.value;
            else errFlg = true;
        }
        else
            errFlg = true;
        
        if (errFlg) Debug.LogError("sender が指定されていないか、不正です");
    }
}
