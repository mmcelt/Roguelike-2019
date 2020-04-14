using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	#region Fields

	public static AudioManager Instance;

	public AudioSource _levelMusic, _gameOverMusic, _winMusic;

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
	#endregion

	#region Private Methods


	#endregion
}
