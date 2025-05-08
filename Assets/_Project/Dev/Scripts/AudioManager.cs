using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixerGroup m_MixerGroup;

    public Sound[] m_Sounds;

    public GameSettings m_Settings;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in m_Sounds)
        {
            s.m_Source = gameObject.AddComponent<AudioSource>();
            s.m_Source.clip = s.m_Clip;
            s.m_Source.loop = s.m_Loop;

            s.m_Source.outputAudioMixerGroup = m_MixerGroup;
        }
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(m_Sounds, item => item.m_Name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        s.m_Source.volume = s.m_Volume * (1f + UnityEngine.Random.Range(-s.m_VolumeVariance / 2f, s.m_VolumeVariance / 2f));
        s.m_Source.pitch = s.m_Pitch * (1f + UnityEngine.Random.Range(-s.m_PitchVariance / 2f, s.m_PitchVariance / 2f));

        if (!s.m_Music)
        {
            if (m_Settings.Audio)
            {
                s.m_Source.Play();
            }
        }
        else
        {
            if (m_Settings.Music)
            {
                s.m_Source.Play();
            }
        }

    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(m_Sounds, item => item.m_Name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        s.m_Source.Stop();
    }

    public void StopAllSounds()
    {
        foreach (Sound s in m_Sounds)
        {
            s.m_Source.Stop();
        }
    }

    public bool IsPlaying(string sound)
    {
        Sound s = Array.Find(m_Sounds, item => item.m_Name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return false;
        }

        return s.m_Source.isPlaying;
    }

}