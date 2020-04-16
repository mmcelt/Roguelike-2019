using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _anyKeyText;
	[SerializeField] string _mainMenuScene;
	[SerializeField] float _waitForAnyKey = 2f;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		Time.timeScale = 1f;
		StartCoroutine(VictorySequencer());
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	IEnumerator VictorySequencer()
	{
		yield return new WaitForSeconds(_waitForAnyKey);

		_anyKeyText.SetActive(true);

		yield return new WaitUntil(() => Input.anyKeyDown);

		SceneManager.LoadScene(_mainMenuScene);
	}
	#endregion
}
