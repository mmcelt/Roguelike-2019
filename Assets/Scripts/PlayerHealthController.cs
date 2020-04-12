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
	}
	#endregion

	#region Private Methods


	#endregion
}
