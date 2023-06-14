using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public List<Sound> sounds;

    public static AudioController instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = sounds.Where(i => i.name == name).FirstOrDefault();
        if (s == null)
        {
            Debug.Log("Sound: " + name + " was not found.");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = sounds.Where(i => i.name == name).FirstOrDefault();
        if (s == null)
        {
            Debug.Log("Sound: " + name + " was not found.");
            return;
        }
        s.source.Stop();
    }
}
