using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	#region Fields

	[SerializeField] string _levelToLoad;

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
	#endregion

	#region Private Methods


	#endregion
}
