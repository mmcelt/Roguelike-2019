using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
	#region Fields

	public static PlayerHealthController Instance;

	[SerializeField] int _maxHealth;
	[SerializeField] int _currentHealth;
	public float _invincibilityLength = 1f;

	float _invincibilityCounter;
	Color _originalBodyColor;

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
		_originalBodyColor = PlayerController.Instance._theSprite.color;
	}

	void Update()
	{
		if (_invincibilityCounter > 0)
		{
			_invincibilityCounter -= Time.deltaTime;

			if (_invincibilityCounter <= 0)
				PlayerController.Instance._theSprite.color = _originalBodyColor;
		}
	}
	#endregion

	#region Public Methods

	public void DamagePlayer()
	{
		if (_invincibilityCounter <= 0)
		{
			_currentHealth--;

			_invincibilityCounter = _invincibilityLength;
			PlayerController.Instance._theSprite.color = new Color(_originalBodyColor.r, _originalBodyColor.g, _originalBodyColor.b, 0.5f);

			if (_currentHealth <= 0)
			{
				_currentHealth = 0;

				PlayerController.Instance.gameObject.SetActive(false);
				UIController.Instance._deathScreen.SetActive(true);
			}
			UpdateHalthbar();
		}
	}

	public void MakeInvincible(float length)
	{
		_invincibilityCounter = length;
		PlayerController.Instance._theSprite.color = new Color(_originalBodyColor.r, _originalBodyColor.g, _originalBodyColor.b, 0.5f);
	}

	public void HealPlayer(int amount)
	{
		_currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
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
