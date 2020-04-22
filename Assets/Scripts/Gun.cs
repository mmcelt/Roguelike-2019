using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	#region Fields

	[Header("Shooting")]
	[SerializeField] GameObject _bulletPrefab;
	[SerializeField] Transform _firePoint;
	[SerializeField] float _timeBetweenShots;
	float _shotCounter;
	[SerializeField] int _shootSFX;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (!PlayerController.Instance._canMove || LevelManager.Instance._isPaused) return;

		//fire bullet...
		if (Input.GetMouseButtonDown(0))
		{
			Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
			_shotCounter = _timeBetweenShots;
			AudioManager.Instance.PlaySFX(_shootSFX);
		}

		if (Input.GetMouseButton(0))
		{
			_shotCounter -= Time.deltaTime;

			if (_shotCounter <= 0)
			{
				Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
				_shotCounter = _timeBetweenShots;
				AudioManager.Instance.PlaySFX(_shootSFX);
			}
		}

	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
