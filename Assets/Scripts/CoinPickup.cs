using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
	#region Fields

	[SerializeField] int _coinValue = 1;
	[SerializeField] float _pickupDelayTime = 0.5f;
	[SerializeField] int _coinPickupSFX;
	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}

	void Update()
	{
		if (_pickupDelayTime > 0)
		{
			_pickupDelayTime -= Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && _pickupDelayTime <= 0)
		{
			LevelManager.Instance.GetCoins(_coinValue);
			AudioManager.Instance.PlaySFX(_coinPickupSFX);

			Destroy(gameObject);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
