﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
	#region Fields

	//[SerializeField] string _levelToLoad;

	#endregion

	#region MonoBehaviour Methods

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			//SceneManager.LoadScene(_levelToLoad);
			StartCoroutine(LevelManager.Instance.LevelEnd());
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
