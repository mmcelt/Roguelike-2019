using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
	#region Fields

	[SerializeField] Gun _theGun;
	[SerializeField] float _pickupDelayTime = 0.5f;
	[SerializeField] int _gunPickupSFX;

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
			bool hasGun = false;
			foreach(Gun gunToCheck in PlayerController.Instance._availableGuns)
			{
				if (_theGun._weaponName == gunToCheck._weaponName)
				{
					hasGun = true;
				}
			}

			if (!hasGun)
			{
				Gun newGun = Instantiate(_theGun, PlayerController.Instance._gunHand);
				PlayerController.Instance._availableGuns.Add(newGun);
				PlayerController.Instance.CurrentGun = PlayerController.Instance._availableGuns.Count - 1;

				PlayerController.Instance.SwitchGun();
			}

			AudioManager.Instance.PlaySFX(_gunPickupSFX);

			Destroy(gameObject);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
