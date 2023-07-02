using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public enum SoundType
{
    BackGroundMusic,
    SoundEffect
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] private Sound[] backgroundMusics = null;
    [SerializeField] private Sound[] soundEffects = null;

    [SerializeField][Range(1, 50)] private int numberofBackgroundMusicPlayers = 5;
    [SerializeField][Range(1, 50)] private int numberofSoundEffectPlayers =     5;

    [SerializeField] private GameObject soundSourcePrefab;

    private SoundSource[] backgroundMusicPlayers;
    private SoundSource[] soundEffectPlayers;

    private float _masterVolume;
    private float _backgroundMusicVolume;
    private float _soundEffectVolume;

    [HideInInspector]
    public float masterVolume
    {
        get { return _masterVolume; }
        set
        {
            if (value > 1f) _masterVolume = 1f;
            else if (value < 0f) _masterVolume = 0f;
            else _masterVolume = value;
            SetSoundSourcesVolumes();
        }
    }
    [HideInInspector]
    public float backgroundMusicVolume
    {
        get { return _backgroundMusicVolume; }
        set
        {
            if (value > 1f) _backgroundMusicVolume = 1f;
            else if (value < 0f) _backgroundMusicVolume = 0f;
            else _backgroundMusicVolume = value;
            SetSoundSourcesVolumes();
        }
    }
    [HideInInspector]
    public float soundEffectVolume
    {
        get { return _soundEffectVolume; }
        set
        {
            if (value > 1f) _soundEffectVolume = 1f;
            else if (value < 0f) _soundEffectVolume = 0f;
            else _soundEffectVolume = value;
            SetSoundSourcesVolumes();
        }
    }

    private void Start()
    {
        //배열 초기화
        backgroundMusicPlayers = new SoundSource[numberofBackgroundMusicPlayers];
        for(int i = 0; i < numberofBackgroundMusicPlayers; i++)
        {
            GameObject go = Instantiate(soundSourcePrefab);
            go.transform.parent = transform;
            backgroundMusicPlayers[i] = go.GetComponent<SoundSource>();
        }
        soundEffectPlayers = new SoundSource[numberofSoundEffectPlayers];
        for (int i = 0; i < numberofSoundEffectPlayers; i++)
        {
            GameObject go = Instantiate(soundSourcePrefab);
            go.transform.parent = transform;
            soundEffectPlayers[i] = go.GetComponent<SoundSource>();
        }

        //test settings
        masterVolume = 1;
        backgroundMusicVolume = 1;
        soundEffectVolume = 1;
    }

    //사운드 재생
    public void PlaySound(string soundName, SoundType soundType, bool mute = false, bool bypassEffects = false,
        bool bypassListenerEffects = false, bool bypassRevervZones = false, int priority = 128, float volume = 1,
        float pitch = 1, float panStero = 0, float spatialBlend = 0, float reverbZoneMix = 1)
    {
        if(soundType == SoundType.BackGroundMusic)
        {
            AudioClip audioClip = null;
            foreach (Sound sound in backgroundMusics)
            {
                if(sound.name == soundName)
                {
                    audioClip = sound.clip;
                }
            }
            if(audioClip == null)
            {
                Debug.LogWarning(soundName + "와(과) 일치하는 BackgroundMusic이 없습니다.");
                return;
            }
            foreach (SoundSource player in backgroundMusicPlayers)
            {
                if (!player.isPlaying)
                {
                    player.clip = audioClip;
                    player.mute = mute;
                    player.bypassEffects = bypassEffects;
                    player.bypassListenerEffects = bypassListenerEffects;
                    player.bypassReverbZones = bypassRevervZones;
                    player.loop = true;
                    player.priority = priority;
                    player.localVolume = volume;
                    player.pitch = pitch;
                    player.panStereo = panStero;
                    player.spatialBlend = spatialBlend;
                    player.reverbZoneMix = reverbZoneMix;
                    player.Play();
                    return;
                }
            }
            Debug.LogWarning((numberofBackgroundMusicPlayers + 1) + "개 이상의 배경음악을 동시에 재생할 수 없습니다.");
        }
        else if(soundType == SoundType.SoundEffect)
        {
            AudioClip audioClip = null;
            foreach (Sound sound in soundEffects)
            {
                if (sound.name == soundName)
                {
                    audioClip = sound.clip;
                    break;
                }
            }
            if (audioClip == null)
            {
                Debug.LogWarning(soundName + "와(과) 일치하는 SoundEffect가 없습니다.");
                return;
            }
            foreach (SoundSource player in soundEffectPlayers)
            {
                if (!player.isPlaying)
                {
                    player.clip = audioClip;
                    player.mute = mute;
                    player.bypassEffects = bypassEffects;
                    player.bypassListenerEffects = bypassListenerEffects;
                    player.bypassReverbZones = bypassRevervZones;
                    player.loop = false;
                    player.priority = priority;
                    player.localVolume = volume;
                    player.pitch = pitch;
                    player.panStereo = panStero;
                    player.spatialBlend = spatialBlend;
                    player.reverbZoneMix = reverbZoneMix;
                    player.Play();
                    return;
                }
            }
            Debug.LogWarning((numberofSoundEffectPlayers + 1) + "개 이상의 음향효과를 동시에 재생할 수 없습니다.");
        }
    }

    //soundName을 재생 중인 플레이어의 음향값을 수정
    public void EditSound(string soundName, bool mute = false, bool bypassEffects = false,
        bool bypassListenerEffects = false, bool bypassRevervZones = false, int priority = 128,
        float volume = 1, float pitch = 1, float panStero = 0, float spatialBlend = 0, float reverbZoneMix = 1)
    {
        AudioClip audioClip = null;
        foreach (Sound sound in backgroundMusics)
        {
            if (sound.name == soundName)
            {
                audioClip = sound.clip;
                break;
            }
        }
        foreach (Sound sound in soundEffects)
        {
            if (sound.name == soundName)
            {
                audioClip = sound.clip;
                break;
            }
        }
        if (audioClip == null)
        {
            Debug.LogWarning(soundName + "와(과) 일치하는 BackgroundMusic 및 SoundEffect가 없습니다.");
            return;
        }
        bool isExist = false;
        foreach (SoundSource player in backgroundMusicPlayers)
        {
            if (player.isPlaying && player.clip == audioClip)
            {
                player.mute = mute;
                player.bypassEffects = bypassEffects;
                player.bypassListenerEffects = bypassListenerEffects;
                player.bypassReverbZones = bypassRevervZones;
                player.loop = true;
                player.priority = priority;
                player.localVolume = volume;
                player.pitch = pitch;
                player.panStereo = panStero;
                player.spatialBlend = spatialBlend;
                player.reverbZoneMix = reverbZoneMix;
                isExist = true;
            }
        }
        foreach (SoundSource player in soundEffectPlayers)
        {
            if (player.isPlaying && player.clip == audioClip)
            {
                player.mute = mute;
                player.bypassEffects = bypassEffects;
                player.bypassListenerEffects = bypassListenerEffects;
                player.bypassReverbZones = bypassRevervZones;
                player.loop = false;
                player.priority = priority;
                player.localVolume = volume;
                player.pitch = pitch;
                player.panStereo = panStero;
                player.spatialBlend = spatialBlend;
                player.reverbZoneMix = reverbZoneMix;
                isExist = true;
            }
        }
        if (!isExist)
        {
            Debug.LogWarning(soundName + "을(를) 재생 중인 플레이어가 없습니다.");
        }
    }

    //soundType을 재생 중인 플레이어의 음향값을 수정
    public void EditSound(SoundType soundType, bool mute = false, bool bypassEffects = false,
        bool bypassListenerEffects = false, bool bypassRevervZones = false, int priority = 128,
        float volume = 1, float pitch = 1, float panStero = 0, float spatialBlend = 0, float reverbZoneMix = 1)
    {
        if (soundType == SoundType.BackGroundMusic)
        {
            int count = 0;
            foreach (SoundSource player in backgroundMusicPlayers)
            {
                if (player.isPlaying)
                {
                    player.mute = mute;
                    player.bypassEffects = bypassEffects;
                    player.bypassListenerEffects = bypassListenerEffects;
                    player.bypassReverbZones = bypassRevervZones;
                    player.loop = true;
                    player.priority = priority;
                    player.localVolume = volume;
                    player.pitch = pitch;
                    player.panStereo = panStero;
                    player.spatialBlend = spatialBlend;
                    player.reverbZoneMix = reverbZoneMix;
                    count++;
                }
            }
            Debug.Log(count + "개의 플레이어값을 수정하였습니다.");
        }
        else if (soundType == SoundType.SoundEffect)
        {
            int count = 0;
            foreach (SoundSource player in soundEffectPlayers)
            {
                if (player.isPlaying)
                {
                    player.mute = mute;
                    player.bypassEffects = bypassEffects;
                    player.bypassListenerEffects = bypassListenerEffects;
                    player.bypassReverbZones = bypassRevervZones;
                    player.loop = false;
                    player.priority = priority;
                    player.localVolume = volume;
                    player.pitch = pitch;
                    player.panStereo = panStero;
                    player.spatialBlend = spatialBlend;
                    player.reverbZoneMix = reverbZoneMix;
                    count++;
                }
            }
            Debug.Log(count + "개의 플레이어값을 수정하였습니다.");
        }
    }

    //soundName을 재생 중인 플레이어를 멈춤
    public void StopSound(string soundName)
    {
        AudioClip audioClip = null;
        foreach (Sound sound in backgroundMusics)
        {
            if (sound.name == soundName)
            {
                audioClip = sound.clip;
                break;
            }
        }
        foreach (Sound sound in soundEffects)
        {
            if (sound.name == soundName)
            {
                audioClip = sound.clip;
                break;
            }
        }
        bool isExist = false;
        foreach (SoundSource player in backgroundMusicPlayers)
        {
            if (player.isPlaying && player.clip == audioClip)
            {
                player.Stop();
                isExist = true;
            }
        }
        foreach (SoundSource player in soundEffectPlayers)
        {
            if (player.isPlaying && player.clip == audioClip)
            {
                player.Stop();
                isExist = true;
            }
        }
        if (!isExist)
        {
            Debug.LogWarning(soundName + "을(를) 재생 중인 플레이어가 없습니다.");
        }
    }

    //재생 중인 모든 플레이어를 멈춤
    public void StopSound()
    {
        int count = 0;
        foreach (SoundSource player in backgroundMusicPlayers)
        {
            if (player.isPlaying)
            {
                player.Stop();
                count++;
            }
        }
        foreach (SoundSource player in soundEffectPlayers)
        {
            if (player.isPlaying)
            {
                player.Stop();
                count++;
            }
        }
        Debug.Log(count + "개의 플레이어를 멈추었습니다.");
    }

    //soundType과 맞는 모든 플레이어를 멈춤
    public void StopSound(SoundType soundType)
    {
        if(soundType == SoundType.BackGroundMusic)
        {
            int count = 0;
            foreach (SoundSource player in backgroundMusicPlayers)
            {
                if (player.isPlaying)
                {
                    player.Stop();
                    count++;
                }
            }
            Debug.Log(count + "개의 플레이어를 멈추었습니다.");
        }
        else if (soundType == SoundType.SoundEffect)
        {
            int count = 0;
            foreach (SoundSource player in soundEffectPlayers)
            {
                if (player.isPlaying)
                {
                    player.Stop();
                    count++;
                }
            }
            Debug.Log(count + "개의 플레이어를 멈추었습니다.");
        }
    }

    private void SetSoundSourcesVolumes()
    {
        foreach (SoundSource player in backgroundMusicPlayers)
        {
            player.globalVolume = _backgroundMusicVolume * _masterVolume;
        }
        foreach (SoundSource player in soundEffectPlayers)
        {
            player.globalVolume = _soundEffectVolume * _masterVolume;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) SoundManager.instance.PlaySound("ForestVillage", SoundType.BackGroundMusic);
        if (Input.GetKeyDown(KeyCode.M)) SoundManager.instance.PlaySound("GoodMemory", SoundType.BackGroundMusic);

        if (Input.GetKeyDown(KeyCode.J)) SoundManager.instance.EditSound("GoodMemory", volume:1f, pitch:3);
        if (Input.GetKeyDown(KeyCode.K)) SoundManager.instance.EditSound("GoodMemory", volume:1f);
        if (Input.GetKeyDown(KeyCode.Space)) SoundManager.instance.StopSound("GoodMemory");
    }
}
