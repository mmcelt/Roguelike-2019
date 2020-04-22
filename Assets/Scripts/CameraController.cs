using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	#region Fields

	public static CameraController Instance;

	[SerializeField] float _moveSpeed;
	[SerializeField] Camera _mainCamera, _bigMapCamera;
	bool _bigMapActive;
	Transform _target;

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
		if (_target == null) return;

		transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.position.x, _target.position.y, transform.position.z), _moveSpeed * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.M))
		{
			if (!_bigMapActive)
				ActivateBigMap();
			else
				DeactivateBigMap();
		}
	}
	#endregion

	#region Public Methods

	public void ChangeTarget(Transform newTarget)
	{
		_target = newTarget;
	}

	public void ActivateBigMap()
	{
		if (LevelManager.Instance._isPaused) return;

		_bigMapCamera.enabled = true;
		_mainCamera.enabled = false;
		PlayerController.Instance._canMove = false;
		UIController.Instance._bigMapInfoText.SetActive(true);
		UIController.Instance._miniMapDisplay.SetActive(false);
		_bigMapActive = true;
		Time.timeScale = 0f;
	}

	public void DeactivateBigMap()
	{
		if (LevelManager.Instance._isPaused) return;

		_bigMapCamera.enabled = false;
		_mainCamera.enabled = true;
		UIController.Instance._bigMapInfoText.SetActive(false);
		UIController.Instance._miniMapDisplay.SetActive(true);
		_bigMapActive = false;
		Time.timeScale = 1f;
	}
	#endregion

	#region Private Methods


	#endregion
}
