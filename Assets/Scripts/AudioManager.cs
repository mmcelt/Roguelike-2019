using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	#region Fields

	public static AudioManager Instance;

	public AudioSource _levelMusic, _gameOverMusic, _winMusic;
	public AudioSource[] _sfx;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Instance = this;
	}

	void Start() 
	{
		
	}
	#endregion

	#region Public Methods

	public void PlayGameOver()
	{
		_levelMusic.Stop();
		_gameOverMusic.Play();
	}

	public void PlayLevelWinMusic()
	{
		_levelMusic.Stop();
		_winMusic.Play();
	}

	public void PlaySFX(int sfxIndex)
	{
		_sfx[sfxIndex].Stop();
		_sfx[sfxIndex].Play();
	}
	#endregion

	#region Private Methods


	#endregion
}
