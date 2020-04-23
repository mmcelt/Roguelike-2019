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
	public int _currentCoins;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Instance = this;
	}

	void Start() 
	{
		_currentCoins = CharacterTracker.Instance._currentCoins;

		Time.timeScale = 1f;
		GetCoins(0);
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

		CharacterTracker.Instance._currentCoins = _currentCoins;
		CharacterTracker.Instance._currentHealth = PlayerHealthController.Instance._currentHealth;
		CharacterTracker.Instance._maxHealth = PlayerHealthController.Instance._maxHealth;

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

	public void GetCoins(int amount)
	{
		_currentCoins += amount;
		UIController.Instance._goldText.text = _currentCoins.ToString();
	}

	public void SpendCoins(int amount)
	{
		_currentCoins = Mathf.Max(_currentCoins -= amount, 0);
		UIController.Instance._goldText.text = _currentCoins.ToString();
	}
	#endregion

	#region Private Methods


	#endregion
}
