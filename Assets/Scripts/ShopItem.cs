using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _buyMessage;
	[SerializeField] int _itemCost;
	[SerializeField] bool _isHealthRestore, _isHealthUpgrade, _isWeapon;

	bool _inBuyZone;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
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
						PlayerHealthController.Instance.HealPlayer(PlayerHealthController.Instance._maxHealth);
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
