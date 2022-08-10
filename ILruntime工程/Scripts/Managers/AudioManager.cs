using UnityEngine;
using System.Collections.Generic;
using FairyGUI;
using System;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    private static AudioListener _listener;
    private static AudioClip mPreMusic;
    
    private Queue<GameObject> _audioSourcePool = new Queue<GameObject>();
    private List<int> waitToRemove = new List<int>();
    /// <summary>
    /// Singleton.
    /// </summary>
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject audioManager = new GameObject("AudioManager");
                _instance = audioManager.AddComponent<AudioManager>();
                _listener = audioManager.AddComponent<AudioListener>();
                GameObject music = new GameObject("backSound");
                music.transform.parent = _instance.transform;
                music.transform.localPosition = Vector3.zero;
                _instance.backMusicSource = music.AddComponent<AudioSource>();
                GameObject sounds = new GameObject("effectSounds");
                _instance.mSoundsTransform = sounds.transform;
                sounds.transform.parent = _instance.transform;
                sounds.transform.localPosition = Vector3.zero;
                GameObject cv = new GameObject("cvSound");
                cv.transform.parent = _instance.transform;
                cv.transform.localPosition = Vector3.zero;
                _instance.cvSource = cv.AddComponent<AudioSource>();

                // 音效开关。
                if (PlayerPrefs.HasKey("SoundMute"))
                    _instance.SoundMute = PlayerPrefs.GetInt("SoundMute") == 1 ? true : false;
                else
                    _instance.SoundMute = false;
                if (PlayerPrefs.HasKey("EffectSoundsVolume"))
                    _instance.EffectSoundsVolume = PlayerPrefs.GetFloat("EffectSoundsVolume");

                // 背景音乐开关。
                if (PlayerPrefs.HasKey("MusicMute"))
                    _instance.MusicMute = PlayerPrefs.GetInt("MusicMute") == 1 ? true : false;
                else
                    _instance.MusicMute = false;
                if (PlayerPrefs.HasKey("BackMusicVolume"))
                    _instance.BackMusicVolume = PlayerPrefs.GetFloat("BackMusicVolume");

                GameObject.DontDestroyOnLoad(audioManager);
            }

            return _instance;
        }
    }




    AudioSource backMusicSource;
    AudioSource cvSource;
    Dictionary<int, AudioSource> effectSoundDict = new Dictionary<int, AudioSource>();
    Transform mSoundsTransform;

    private bool _applicationPaused = false;
    private bool _soundPaused = false;
    private bool _soundMute = false;
    private bool _musicMute = false;

    private GameObject GetPoolAudioSourceObject(out AudioSource audioSource)
    {
        GameObject sound = null;
        if (_audioSourcePool.Count > 0)
        {
            sound = _audioSourcePool.Dequeue();
            audioSource = sound.GetComponent<AudioSource>();
        }
        else
        {
            sound = new GameObject("Sound");
            audioSource = sound.AddComponent<AudioSource>();
            sound.transform.parent = mSoundsTransform;
            sound.transform.localPosition = Vector3.zero;
        }
        sound.SetActive(true);
        return sound;
    }

    private void ReturnPoolAudioSourceObject(GameObject sound)
    {
        if (sound != null)
        {
            sound.SetActive(false);
            _audioSourcePool.Enqueue(sound);
        }
    }

    #region Sounds.
    /// <summary>
    /// 音效静音开关。
    /// True为静音。
    /// False为不静音。
    /// </summary>
    public bool SoundMute
    {
        get
        {
            return _soundMute;
        }
        set
        {
            _soundMute = value;
            //if (_soundMute)
            //    GRoot.inst.EnableSound();
            //else
            //    GRoot.inst.DisableSound();

            //调整全局声音音量，这个包括按钮声音和动效播放的声音
            GRoot.inst.soundVolume = _soundMute ? 1 : 0;
            foreach (AudioSource audioSource in effectSoundDict.Values)
            {
                audioSource.mute = _soundMute;
            }
            PlayerPrefs.SetInt("SoundMute", _soundMute ? 1 : 0);
        }
    }


    /// <summary>
    /// 播放音效。
    /// </summary>
    /// <param name="audioClip">要播放的音效。</param>
    /// <returns>返回播放的id，可以用于删除该音效。</returns>
    public void PlaySound(AudioClip audioClip, double startTime = 0)
    {
        PlaySound(audioClip, false, startTime);
    }

    public void PlaySound(NAudioClip audioClip, double startTime = 0)
    {
        PlaySound(audioClip.nativeClip, false, startTime);
    }

    /// <summary>
    /// 播放音效。
    /// </summary>
    /// <param name="audioClip">要播放的音效。</param>
    /// <param name="isLoop">是否循环播放。</param>
    /// <returns>返回播放的id，可以用于删除该音效。</returns>
    public void PlaySound(AudioClip audioClip, bool isLoop, double startTime = 0)
    {
        cvSource.Stop();
        cvSource.clip = audioClip;
        cvSource.loop = isLoop;
        cvSource.mute = SoundMute;
        cvSource.volume = EffectSoundsVolume;
        cvSource.PlayScheduled(startTime);
    }

    public void PlayFairyGUISound(string pkgDirectory, string bundleName, string audioName)
    {
        if (string.IsNullOrEmpty(audioName))
        {
            Utils.Log("PlayFairyGUISound : Resource not found -  in Music");
            return;
        }
        NAudioClip audioClip = AssetManager.Instance.GetItemAssetFromResName(pkgDirectory, bundleName, audioName) as NAudioClip;
        if (audioClip != null)
        {
            PlaySound(audioClip, 0);
        }
    }

    public void PlaySound(string path, bool isLoop = false, double startTime = 0)
    {
        AudioClip ac = (AudioClip)Resources.Load(path, typeof(AudioClip));
        PlaySound(ac, isLoop, startTime);
    }

    /// <summary>
    /// 暂停音效播放，暂停后可以恢复，停止后就不能恢复了。
    /// </summary>
    public void PauseSounds()
    {
        foreach (AudioSource audioSource in effectSoundDict.Values)
        {
            audioSource.Pause();
        }
        _soundPaused = true;
    }

    /// <summary>
    /// 继续播放音效。
    /// </summary>
    public void ContinueSounds()
    {
        foreach (AudioSource audioSource in effectSoundDict.Values)
        {
            audioSource.Play();
        }
        _soundPaused = false;
    }

    /// <summary>
    /// 停止播放指定的音效。停止后不能恢复。
    /// </summary>
    /// <param name="soundId">指定要停止的音效Id。对应PlaySound的返回值。</param>
    public void StopSound(int soundId)
    {
        if (effectSoundDict.ContainsKey(soundId))
        {
            AudioSource audioSource = effectSoundDict[soundId];
            effectSoundDict.Remove(soundId);
            GameObject.Destroy(audioSource.gameObject);
        }
    }

    /// <summary>
    /// 停止播放所有音效。停止后不能恢复。
    /// </summary>
    public void StopSounds()
    {
        foreach (AudioSource audioSource in effectSoundDict.Values)
        {
            GameObject.Destroy(audioSource.gameObject);
        }
        effectSoundDict.Clear();
    }

    private float _effectSoundsVolume = 1;
    /// <summary>
    /// 设置音效的音量
    /// </summary>
    public float EffectSoundsVolume
    {
        get
        {
            return _effectSoundsVolume;
        }
        set
        {
            if (_effectSoundsVolume != value)
            {
                _effectSoundsVolume = value;
                PlayerPrefs.SetFloat("EffectSoundsVolume", _effectSoundsVolume);
                foreach (AudioSource audioSource in effectSoundDict.Values)
                {
                    audioSource.volume = _effectSoundsVolume;
                }
            }
        }
    }

    #endregion





    #region Music.
    /// <summary>
    /// 背景音乐静音。
    /// True为静音。
    /// False为不静音。
    /// </summary>
    public bool MusicMute
    {
        get { return _musicMute; }
        set
        {
            _musicMute = value;
            backMusicSource.mute = _musicMute;
            PlayerPrefs.SetInt("MusicMute", _musicMute ? 1 : 0);
        }
    }

    /// <summary>
    /// 播放背景音效。
    /// </summary>
    /// <param name="audioClip">音效文件。</param>
    /// <param name="isLoop">是否循环播放。</param>
    /// <param name="replaySameSong">如果与当前是同一音效，是重新播放还是继续播放。</param>
    /// <param name="isRecord">是否记录前一个声音文件</param>
    public void PlayMusic(AudioClip audioClip, bool isLoop = true, bool replaySameSong = false, bool isRecord = false)
    {
        if (isRecord)
        {
            if (null != backMusicSource.clip && null == mPreMusic)
            {
                mPreMusic = backMusicSource.clip;
            }
        }

        if (replaySameSong)
        {
            backMusicSource.Stop();
            backMusicSource.loop = isLoop;
            backMusicSource.clip = audioClip;
            backMusicSource.mute = _musicMute;
            backMusicSource.Play();
        }
        else
        {
            backMusicSource.loop = isLoop;
            if (backMusicSource.clip != audioClip)
            {
                backMusicSource.Stop();
                backMusicSource.clip = audioClip;
                backMusicSource.mute = _musicMute;
                backMusicSource.Play();
            }
        }
    }
    public void PlayMusic(NAudioClip audioClip, bool isLoop = true, bool replaySameSong = false, bool isRecord = false)
    {
        PlayMusic(audioClip.nativeClip, isLoop, replaySameSong, isRecord);
    }
    /// <summary>
    /// 播放背景音乐。
    /// </summary>
    /// <param name="audioClip">音效文件资源路径。</param>
    /// <param name="isLoop">是否循环播放。</param>
    /// <param name="replaySameSong">如果与当前是同一音效，是重新播放还是继续播放。</param>
    public void PlayMusic(string strPath, bool isLoop = true, bool replaySameSong = false, bool isRecord = false)
    {
        AudioClip ac = (AudioClip)Resources.Load(strPath, typeof(AudioClip));
        this.PlayMusic(ac, isLoop, replaySameSong, isRecord);
    }

    public void PlayFairyGUIMusic(string pkgDirectory, string bundleName, string audioName, bool isLoop = true, bool replaySameSong = false, bool isRecord = false)
    {
        if (string.IsNullOrEmpty(audioName))
        {
            Utils.Log("PlayFairyGUIMusic : Resource not found -  in Music");
            return;
        }
        NAudioClip ac = AssetManager.Instance.GetItemAssetFromResName(pkgDirectory, bundleName, audioName) as NAudioClip;
        if (ac != null)
        {
            this.PlayMusic(ac, isLoop, replaySameSong, isRecord);
        }
    }

    /// <summary>
    /// 暂停背景音乐。
    /// </summary>
    public void PauseMusic()
    {
        backMusicSource.Pause();
    }

    /// <summary>
    /// 继续播放背景音乐。
    /// </summary>
    public void ContinueMusic()
    {
        backMusicSource.Play();
    }

    /// <summary>
    /// 停止播放背景音乐。
    /// </summary>
    public void StopMusic()
    {
        backMusicSource.Stop();
        backMusicSource.clip = null;
    }

    private float _backMusicVolume = 1;
    /// <summary>
    /// 设置背景音乐的音量大小
    /// </summary>
    /// <param name="volume"></param>
    public float BackMusicVolume
    {
        get
        {
            return _backMusicVolume;
        }
        set
        {
            _backMusicVolume = value;
            backMusicSource.volume = value;
            PlayerPrefs.SetFloat("BackMusicVolume", _backMusicVolume);
        }
    }

    #endregion


    void OnApplicationPause(bool pause)
    {
        _applicationPaused = pause;
    }

    // Update is called once per frame
    void Update()
    {
        if (!backMusicSource.isPlaying && null != mPreMusic)
        {
            PlayMusic(mPreMusic);
            mPreMusic = null;
        }

        foreach (var item in effectSoundDict)
        {
            if (item.Value == null)
                waitToRemove.Add(item.Key);
            else if (!_soundPaused && !_applicationPaused && !item.Value.isPlaying)
            {
                ReturnPoolAudioSourceObject(item.Value.gameObject);
                waitToRemove.Add(item.Key);
            }
        }
        foreach (int key in waitToRemove)
        {
            effectSoundDict.Remove(key);
        }
    }
}
