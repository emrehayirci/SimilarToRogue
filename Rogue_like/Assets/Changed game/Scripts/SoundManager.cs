using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource musicSource;
    public static SoundManager instance  = null;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    public string roomName;
    public AudioClip[] music;

	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        PlaySong(music,roomName);
	}

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlaySong(AudioClip [] music, string roomName)
    {
        //let it choose a random song of the level songs, all other songs are for specific uses.
        int randomSong = Random.Range(0, music.Length - 2);
        int shopTheme = music.Length - 1;
        int bossTheme = music.Length;

        if (roomName == "NotSpecial")
        {
            musicSource.clip = music[randomSong];
        }
        else if(roomName == "Shop")
        {
            musicSource.clip = music[shopTheme];
        }
        else
        {
            musicSource.clip = music[bossTheme];
        }
        musicSource.Play();
        if(!musicSource.isPlaying)
        {
            PlaySong(music, roomName);
        }
    }

    public void RandomizeSFX(params AudioClip [] clips)
    {
        int randomIndex = Random.Range(0,clips.Length);
        float randomPitch = Random.Range(lowPitchRange,highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
}
