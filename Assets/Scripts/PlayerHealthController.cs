using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
	#region Fields

	public static PlayerHealthController Instance;

	[SerializeField] int _maxHealth;
	[SerializeField] int _currentHealth;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Instance = this;
	}

	void Start() 
	{
		_currentHealth = _maxHealth;
		UIController.Instance._healthSlider.maxValue = _maxHealth;
		UpdateHalthbar();
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void DamagePlayer()
	{
		_currentHealth--;

		if (_currentHealth <= 0)
		{
			_currentHealth = 0;

			PlayerController.Instance.gameObject.SetActive(false);
		}
		UpdateHalthbar();
	}
	#endregion

	#region Private Methods

	void UpdateHalthbar()
	{
		UIController.Instance._healthSlider.value = _currentHealth;
		UIController.Instance._healthText.text = _currentHealth + " / " + _maxHealth;
	}
	#endregion
}
