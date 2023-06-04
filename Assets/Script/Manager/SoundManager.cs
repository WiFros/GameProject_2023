using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    private void Awake()
    {
        if(instance == null)
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

    private AudioSource backgroundMusicPlayer;
    private AudioSource[] soundEffectPlayers;

    private float masterVolume = 1f;
    private float backgroundMusicVolume = 1f;
    private float soundEffectVolume = 1f;

    private void Start()
    {
        backgroundMusicPlayer = gameObject.AddComponent<AudioSource>();
        backgroundMusicPlayer.loop = true;
        int numberofSoundEffectPlayers = 5;
        soundEffectPlayers = new AudioSource[numberofSoundEffectPlayers];
        for(int i = 0; i < numberofSoundEffectPlayers; i++)
        {
            soundEffectPlayers[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayBackgroundMusic(string soundName)
    {
        foreach(Sound sound in backgroundMusics)
        {
            if(sound.name == soundName)
            {
                if (backgroundMusicPlayer.isPlaying && backgroundMusicPlayer.clip == sound.clip) return;
                backgroundMusicPlayer.clip = sound.clip;
                backgroundMusicPlayer.Play();
                return;
            }
        }
        Debug.LogWarning(soundName + "와(과) 일치하는 BackgroundMusic이 없습니다.");
    }

    public void PlaySoundEffect(string soundName)
    {
        foreach(Sound sound in soundEffects)
        {
            if(sound.name == soundName)
            {
                foreach(AudioSource player in soundEffectPlayers)
                {
                    if (!player.isPlaying)
                    {
                        player.clip = sound.clip;
                        player.Play();
                        return;
                    }
                }
                Debug.LogWarning((soundEffectPlayers.Length + 1) + "개 이상의 사운드를 동시에 재생할 수 없습니다.");
                return;
            }
        }
        Debug.LogWarning(soundName + "와(과) 일치하는 SoundEffect가 없습니다.");
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicPlayer.Stop();
    }

    public void SetMasterVolume(float volume)
    {
        if (volume > 1f) masterVolume = 1;
        else if (volume < 0f) masterVolume = 0;
        else masterVolume = volume;

        SetAudioSourceVolumes();
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }

    public void SetbackgroundVolume(float volume)
    {
        if (volume > 1f) backgroundMusicVolume = 1;
        else if (volume < 0f) backgroundMusicVolume = 0;
        else backgroundMusicVolume = volume;

        SetAudioSourceVolumes();
    }

    public float GetBackgroundVolume()
    {
        return backgroundMusicVolume;
    }

    public void SetSoundEffectVolume(float volume)
    {
        if (volume > 1f) soundEffectVolume = 1;
        else if (volume < 0f) soundEffectVolume = 0;
        else soundEffectVolume = volume;

        SetAudioSourceVolumes();
    }

    public float GetSoundEffectVolume()
    {
        return soundEffectVolume;
    }

    private void SetAudioSourceVolumes()
    {
        backgroundMusicPlayer.volume = backgroundMusicVolume * masterVolume;
        foreach(AudioSource soundEffectPlayer in soundEffectPlayers)
        {
            soundEffectPlayer.volume = soundEffectVolume * masterVolume;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) SoundManager.instance.PlayBackgroundMusic("ForestVillage");
        if (Input.GetKeyDown(KeyCode.M)) SoundManager.instance.PlayBackgroundMusic("GoodMemory");

        if (Input.GetKeyDown(KeyCode.Y)) SoundManager.instance.SetMasterVolume(GetMasterVolume() + 0.1f);
        if (Input.GetKeyDown(KeyCode.H)) SoundManager.instance.SetMasterVolume(GetMasterVolume() - 0.1f);
        if (Input.GetKeyDown(KeyCode.U)) SoundManager.instance.SetbackgroundVolume(GetBackgroundVolume() + 0.1f);
        if (Input.GetKeyDown(KeyCode.J)) SoundManager.instance.SetbackgroundVolume(GetBackgroundVolume() - 0.1f);
        if (Input.GetKeyDown(KeyCode.I)) SoundManager.instance.SetSoundEffectVolume(GetSoundEffectVolume() + 0.1f);
        if (Input.GetKeyDown(KeyCode.K)) SoundManager.instance.SetSoundEffectVolume(GetSoundEffectVolume() - 0.1f);
    }
}
