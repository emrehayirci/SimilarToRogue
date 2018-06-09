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

    private void Update()
    {

        if (!musicSource.isPlaying)
        {
            PlaySong(music, roomName);
        }

		if (GameObject.FindGameObjectWithTag ("Shop")) {
			if(musicSource.clip != music[music.Length - 2])
				musicSource.Stop ();
			roomName = "Shop";
			if(!musicSource.isPlaying)
				PlaySong (music, roomName);
		} else if (GameObject.FindGameObjectWithTag ("BossMonster")) {
			if(musicSource.clip != music[music.Length - 1])
			if(musicSource.clip != music[music.Length -1])
				musicSource.Stop ();
			roomName = "Boss";
			if(!musicSource.isPlaying)
			PlaySong (music, roomName);
		} else {
			if(musicSource.clip == music[music.Length - 2] ||musicSource.clip == music[music.Length -1])
				musicSource.Stop ();
			roomName = "NotSpecial";
			if(!musicSource.isPlaying)
				PlaySong (music, roomName);
		}
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlaySong(AudioClip [] Newmusic, string NewroomName)
    {
        //let it choose a random song of the level songs, all other songs are for specific uses.
        int randomSong = Random.Range(0, Newmusic.Length - 3);
        int shopTheme = Newmusic.Length - 2;
        int bossTheme = Newmusic.Length - 1;

        if (NewroomName == "NotSpecial")
        {
            musicSource.clip = music[randomSong];
            musicSource.loop = false;
        }
        else if(NewroomName == "Shop")
        {
            musicSource.clip = music[shopTheme];
            musicSource.loop = true;
        }
        else
        {
            musicSource.clip = music[bossTheme];
            musicSource.loop = true;
        }
        musicSource.Play();
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
