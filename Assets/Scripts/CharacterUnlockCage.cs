using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnlockCage : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _message;
	[SerializeField] CharacterSelector[] _charSelects;
	[SerializeField] SpriteRenderer _cagedSprite;

	bool _canUnlock;
	CharacterSelector _characterToUnlock;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_characterToUnlock = _charSelects[Random.Range(0, _charSelects.Length)];
		_cagedSprite.sprite = _characterToUnlock._playerToSpawn._theSprite.sprite;
	}
	
	void Update() 
	{
		if (Input.GetKeyDown(KeyCode.E) && _canUnlock)
		{
			PlayerPrefs.SetInt(_characterToUnlock._playerToSpawn.name, 1);

			Instantiate(_characterToUnlock, transform.position, Quaternion.identity);

			gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canUnlock = true;
			_message.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canUnlock = false;
			_message.SetActive(false);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
