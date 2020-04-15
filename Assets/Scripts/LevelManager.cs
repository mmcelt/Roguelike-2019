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

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Instance = this;
	}

	void Start() 
	{
		
	}
	
	void Update() 
	{
		
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
	#endregion

	#region Private Methods


	#endregion
}
