using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	#region Fields

	public static LevelManager Instance;

	[SerializeField] float _loadWaitTime = 4f;

	public string _nextLevel;
	public bool _isPaused;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Instance = this;
	}

	void Start() 
	{
		Time.timeScale = 1f;
	}
	
	void Update() 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseUnpause();
		}
	}
	#endregion

	#region Public Methods

	public IEnumerator LevelEnd()
	{
		PlayerController.Instance.StopThePlayer();
		AudioManager.Instance.PlayLevelWinMusic();
		UIController.Instance.FadeToBlack();

		yield return new WaitForSeconds(_loadWaitTime);

		SceneManager.LoadScene(_nextLevel);
		PlayerController.Instance._canMove = true;
	}

	public void PauseUnpause()
	{
		if (!_isPaused)
		{
			UIController.Instance._pauseMenu.SetActive(true);
			_isPaused = true;
			Time.timeScale = 0f;

		}
		else
		{
			UIController.Instance._pauseMenu.SetActive(false);
			_isPaused = false;
			Time.timeScale = 1f;
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
