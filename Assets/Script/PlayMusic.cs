using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
	public bool DontDestroy = false;
	public AudioSource _audioSource;
	public AudioClip _audio;
	private GameObject[] other;
	private bool NotFirst = false;
	private void Awake()
	{
		if (DontDestroy) 
		{
			other = GameObject.FindGameObjectsWithTag("Music");

			foreach (GameObject oneOther in other)
			{
				if (oneOther.scene.buildIndex == -1 && oneOther.name == this.gameObject.name)
				{
					NotFirst = true;
				}
			}

			if (NotFirst == true)
			{
				Destroy(gameObject);
			}
			DontDestroyOnLoad(transform.gameObject);
		}	
	}

	public void Play()
	{
		if (_audioSource.isPlaying) return;
		_audioSource.clip = _audio;
		_audioSource.Play();
	}

	public void PlaySelect(AudioClip audio)
	{
		if (_audioSource.isPlaying) 
			_audioSource.Stop();
		_audioSource.clip = audio;
		_audioSource.Play();
	}

	public void Stop()
	{
		_audioSource.Stop();
	}
}
