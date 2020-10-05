using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.Play();
    }

}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds;

    public static SoundManager instance;


    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Debug.Log("Multiple sound managers created");
    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound" + i + sounds[i].name);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("Theme");
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }

        }
    }
}

