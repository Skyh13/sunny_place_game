using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	// if a variable is static, there can be only one.
	public static SoundManager Instance = null;

	public GameObject audioSourcePrefab;
	public AudioSource[] audioSources;
    public int NumOfAudioSources;
    
	public float musicVolume = 0.7f;
	public float sfxVolume = 0.7f;

	AudioSource backgroundMusic = null;

	void Awake () {
		if (Instance == null) {
			DontDestroyOnLoad(this);
			Instance = this;
		}
		else {
			Destroy(this.gameObject);
		}
	}

	void Start () {
		// initialize the array of audiosources
		audioSources = new AudioSource[NumOfAudioSources];

		// populating the array with audiosources by instantiating our audiosource prefab
		for(int i = 0; i < audioSources.Length; i++) {
			audioSources[i] = (Instantiate(audioSourcePrefab) as GameObject).GetComponent<AudioSource>();
			audioSources[i].transform.SetParent(transform);
		}

	}

    void Update()
    {

    }

	public void setMusicVolume(float val) {
		musicVolume = val;
		backgroundMusic.volume = musicVolume;
	}

	public void setSfxVolume(float val) {
		sfxVolume = val;
	}

    // base method for playing a sound. give it a clip, volume, pitch, and optionally a bool for if it should loop
    public AudioSource PlaySound (AudioClip clip, float volume, float pitch, bool loop = false) {
		// calling the GetSourceIndex method from this script that gives us an audiosource that is not currently playing
		int index = GetSourceIndex();
		
		// assign the various details about the audiosource that we passed as arguments to this method
		audioSources[index].clip = clip;
		audioSources[index].volume = volume;
		audioSources[index].pitch = pitch;
		audioSources[index].loop = loop;

		// play the clip
		audioSources[index].Play();

		// return the audiosource we're using
		return audioSources[index];
	}

	// this is an overload method, which just means that we can pass different or fewer arguments
	public AudioSource PlaySound (AudioClip clip) {
		// we just call the first PlaySound method here. it knows to call the one above because we've passed the arguments it's looking for
		return PlaySound(clip, sfxVolume, 1f);
	}

	// method to get the index of an audiosource that isn't playing anything currently
	public int GetSourceIndex () {
		// go through each audiosource
		for(int i = 0; i < audioSources.Length; i++) {
			if (!audioSources[i].isPlaying) {
				// if this one is not playing, good, we'll take it. stop looking and return this index
				return i;
			}
		}

		// if you get here, you've checked all audiosources and they're all playing. returning a default value and sending a console message
		Debug.Log("all audiosources are currently playing, returning index 0");
		return 0;
	}

	public AudioSource PlaySoundJitter (AudioClip clip, float maxVolume, float volumeDelta, float maxPitch, float pitchDelta) {
		float volume = maxVolume - Random.Range(0, volumeDelta);
		float pitch = maxPitch - Random.Range(0, pitchDelta);

		return PlaySound(clip, volume * sfxVolume, pitch);
	}

	public AudioSource PlayMusic(AudioClip clip)
	{
		return backgroundMusic = PlaySound(clip, musicVolume, 1, true);
	}

	// method to stop a sound 
	public void StopSound (AudioSource audioSource) {
		audioSource.Stop();
	}
}






























