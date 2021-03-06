﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _buyMessage;
	[SerializeField] int _itemCost;
	[SerializeField] int _buyItemSFX;
	[SerializeField] int _notEnoughCoinSFX;
	[Header("Health Restore")]
	[SerializeField] bool _isHealthRestore;
	[Header("Health Upgrade")]
	[SerializeField] bool _isHealthUpgrade;
	[SerializeField] int _healthUpgradeAmount;
	[Header("Weapon")]
	[SerializeField] bool _isWeapon;
	[SerializeField] Gun[] _potentialGuns;
	[SerializeField] SpriteRenderer _gunSprite;
	[SerializeField] Text _gunInfoText;

	Gun _theGun;
	bool _inBuyZone;

	#endregion

	#region MonoBehaviour Methods

	void Start()
	{
		if (_isWeapon)
		{
			int selectedGun = Random.Range(0, _potentialGuns.Length);
			_theGun = _potentialGuns[selectedGun];
			_gunSprite.sprite = _theGun._shopSprite;
			_gunInfoText.text = _theGun._weaponName + "\n* " + _theGun._shopCost + " Gold *";
			_itemCost = _theGun._shopCost;
		}
	}

	void Update()
	{
		if (_inBuyZone)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				if(LevelManager.Instance._currentCoins >= _itemCost)
				{
					LevelManager.Instance.SpendCoins(_itemCost);

					if (_isHealthRestore)
					{
						PlayerHealthController.Instance.HealPlayer(PlayerHealthController.Instance.MaxHealth);
					}
					if (_isHealthUpgrade)
					{
						PlayerHealthController.Instance.IncreaseMaxHealth(_healthUpgradeAmount);
						gameObject.SetActive(false);
						_inBuyZone = false;
					}
					if (_isWeapon)
					{
						Gun newGun = Instantiate(_theGun, PlayerController.Instance._gunHand);
						PlayerController.Instance._availableGuns.Add(newGun);
						PlayerController.Instance.CurrentGun = PlayerController.Instance._availableGuns.Count - 1;

						PlayerController.Instance.SwitchGun();

						gameObject.SetActive(false);
						_inBuyZone = false;
					}

					AudioManager.Instance.PlaySFX(_buyItemSFX);
				}
				else
				{
					AudioManager.Instance.PlaySFX(_notEnoughCoinSFX);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_buyMessage.SetActive(true);
			_inBuyZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_buyMessage.SetActive(false);
			_inBuyZone = false;
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
