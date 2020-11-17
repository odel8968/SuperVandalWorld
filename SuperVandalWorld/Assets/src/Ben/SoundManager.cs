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

    //Sets the source as a new game object for sounds without direction
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volume;
    }

    //Plays a sound with no directional source
    public void Play()
    {
        source.Play();
    }

    //Stops the playback of a sound
    public void StopPlaying()
    {
        source.Stop();
    }

    //Plays a sound and sets it to loop indefinitely
    public void PlayLooping()
    {
        source.Play();
        source.loop = true;
    }

    //Plays a sound attached to a game object so that audio can be directional
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

    //Singleton
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            UnityEngine.Debug.Log("Multiple sound managers created");
    }

    //On start, build sound library and start playing the theme for the current level
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound" + i + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        Scene scene = SceneManager.GetActiveScene();
        UnityEngine.Debug.Log("Active Scene is '" + scene.buildIndex + "'.");
        
        if(SceneManager.GetActiveScene().buildIndex == 0)
            PlaySoundLooping("Theme0");
        else if (SceneManager.GetActiveScene().buildIndex == 1)
            PlaySoundLooping("Theme1");
        else if (SceneManager.GetActiveScene().buildIndex == 2)
            PlaySoundLooping("Theme2");
        else if (SceneManager.GetActiveScene().buildIndex == 3)
            PlaySoundLooping("Theme3");
    }

    //Public method to play a sound by name
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

    //Public method to play a sound attached to a game object
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

    //Public method to play a sound that loops forever
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

    //Public method to stop playing a specific sound
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

