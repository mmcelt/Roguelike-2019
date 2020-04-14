using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
	#region Fields

	[SerializeField] int _healAmount = 1;
	[SerializeField] float _pickupDelayTime = 0.5f;

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
			PlayerHealthController.Instance.HealPlayer(_healAmount);
			Destroy(gameObject);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
