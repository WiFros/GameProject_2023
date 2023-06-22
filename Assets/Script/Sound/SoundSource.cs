using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundSource : MonoBehaviour
{
    private AudioSource audioSource;

    private float _localVolume;
    private float _globalVolume;

    public bool isPlaying { get { return audioSource.isPlaying; } }
    public AudioClip clip { get { return audioSource.clip; } set { audioSource.clip = value; } }
    public bool mute { set { audioSource.mute = value; } }
    public bool bypassEffects { set { audioSource.bypassEffects = value; } }
    public bool bypassListenerEffects { set { audioSource.bypassListenerEffects = value; } }
    public bool bypassReverbZones { set { audioSource.bypassReverbZones = value; } }
    public bool loop { set { audioSource.loop = value; } }
    public int priority
    {
        set
        {
            if (value > 256) audioSource.priority = 256;
            else if (value < 0) audioSource.priority = 0;
            else audioSource.priority = value;
        }
    }
    public float localVolume
    {
        set
        {
            if (value > 1) _localVolume = 1;
            else if (value < 0) _localVolume = 0;
            else _localVolume = value;
            audioSource.volume = _localVolume * _globalVolume;
        }
    }
    public float globalVolume
    {
        set
        {
            if (value > 1) _globalVolume = 1;
            else if (value < 0) _globalVolume = 0;
            else _globalVolume = value;
            audioSource.volume = _localVolume * _globalVolume;
        }
    }
    public float pitch
    {
        set
        {
            if (value > 3) audioSource.pitch = 3;
            else if (value < -3) audioSource.pitch = -3;
            else audioSource.pitch = value;
        }
    }
    public float panStereo
    {
        set
        {
            if (value > 1) audioSource.panStereo = 1;
            else if (value < -1) audioSource.panStereo = -1;
            else audioSource.panStereo = value;
        }
    }
    public float spatialBlend
    {
        set
        {
            if (value > 1) audioSource.spatialBlend = 1;
            else if (value < 0) audioSource.spatialBlend = 0;
            else audioSource.spatialBlend = value;
        }
    }
    public float reverbZoneMix
    {
        set
        {
            if (value > 1.1f) audioSource.reverbZoneMix = 1.1f;
            else if (value < 0) audioSource.reverbZoneMix = 0;
            else audioSource.reverbZoneMix = value;
        }
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
