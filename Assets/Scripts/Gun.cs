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
	public string _weaponName;
	public Sprite _gunUI;
	[Header("Shop Data")]
	public int _shopCost;
	public Sprite _shopSprite;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (!PlayerController.Instance._canMove || LevelManager.Instance._isPaused) return;

		//fire bullet...
		if(_shotCounter > 0)
		{
			_shotCounter -= Time.deltaTime;
		}
		else
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
			{
				Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
				AudioManager.Instance.PlaySFX(_shootSFX);
				_shotCounter = _timeBetweenShots;
			}

			//if (Input.GetMouseButton(0))
			//{
			//	Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
			//	AudioManager.Instance.PlaySFX(_shootSFX);
			//_shotCounter=_timeBetweenShots
			//}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
