using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	#region Fields

	[SerializeField] string _levelToLoad;
	[SerializeField] GameObject _warningPanel;
	[SerializeField] CharacterSelector[] _charactersToRelock;

	#endregion

	#region MonoBehaviour Methods

	#endregion

	#region Public Methods

	public void StartGame()
	{
		SceneManager.LoadScene(_levelToLoad);
	}

	public void ExitGame()
	{
#if UNITY_EDITOR

		UnityEditor.EditorApplication.isPlaying = false;

#endif
		Application.Quit();
	}

	public void DeleteSave()
	{
		_warningPanel.SetActive(true);
	}

	public void OkToDelete()
	{
		foreach(CharacterSelector character in _charactersToRelock)
		{
			PlayerPrefs.SetInt(character._playerToSpawn.name, 0);
		}

		_warningPanel.SetActive(false);
	}

	public void CancelDeletion()
	{
		_warningPanel.SetActive(false);
	}
	#endregion

	#region Private Methods


	#endregion
}
