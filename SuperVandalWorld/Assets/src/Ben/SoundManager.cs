using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volume;
    }

    public void Play()
    {
        source.Play();
    }

    public void StopPlaying()
    {
        source.Stop();
    }

    public void PlayLooping()
    {
        source.Play();
        source.loop = true;
    }

    public void PlayOnObject(GameObject _go)
    {
        source = _go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = 1f;
        source.minDistance = 115;
        source.maxDistance = 185;
        source.dopplerLevel = 0;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.reverbZoneMix = 0;

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
            UnityEngine.Debug.Log("Multiple sound managers created");
    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound" + i + sounds[i].name);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        Scene scene = SceneManager.GetActiveScene();
        UnityEngine.Debug.Log("Active Scene is '" + scene.buildIndex + "'.");

        if(SceneManager.GetActiveScene().buildIndex == 1)
            PlaySoundLooping("Theme1");
    }

    /*void Update() //Just for testing stuff
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlaySound("Theme", GameObject.Find("Square"));
            UnityEngine.Debug.Log("sound test");
            UnityEngine.Debug.Log(GameObject.Find("Square"));

            UIManager.instance.AddScore(10);
        }
    }*/

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

    public void PlaySound(string _name, GameObject _object)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].PlayOnObject(_object);
                return;
            }

        }
    }

    public void PlaySoundLooping(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].PlayLooping();
                return;
            }

        }
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].StopPlaying();
                return;
            }

        }
    }
}

