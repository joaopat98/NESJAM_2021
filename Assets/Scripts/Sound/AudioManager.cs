using UnityEngine.Audio;
using UnityEngine;

using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private Sound intro;
    private Sound loop;

    void Awake()
    {
        // Needed if we want the audio manager to persist through scenes (do we?)
        /*
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        */

        // Add audio source components
        foreach (Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.Stop();
    }

    public void SetMusic(string introName, string loopName)
    {
        if (intro != null)
            intro.Stop();
        if (loop != null)
            loop.Stop();

        intro = Array.Find(sounds, sound => sound.name == introName);
        if (intro == null)
        {
            Debug.LogWarning("Sound " + introName + " not found!");
            return;
        }

        loop = Array.Find(sounds, sound => sound.name == loopName);
        if (intro == null)
        {
            Debug.LogWarning("Sound " + loopName + " not found!");
            return;
        }

        double introDuration = intro.clip.length;
        double startTime = AudioSettings.dspTime + 0.2;
        intro.PlayScheduled(startTime);
        loop.PlayScheduled(startTime + introDuration);
    }

    public bool isPlaying(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return false;
        }
        return s.IsPlaying();
    }
}