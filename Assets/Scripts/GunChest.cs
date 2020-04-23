using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{
	#region Fields

	[SerializeField] GunPickup[] _potentialGuns;
	[SerializeField] SpriteRenderer _theSprite;
	[SerializeField] Sprite _chestOpenSprite;
	[SerializeField] GameObject _notification;
	[SerializeField] Transform _spawnPoint;
	[SerializeField] float _scaleSpeed;

	bool _canOpen, _isOpen;

	#endregion

	#region MonoBehaviour Methods

	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && _canOpen && !_isOpen)
		{
			_isOpen = true;
			_theSprite.sprite = _chestOpenSprite;
			int selectedGun = Random.Range(0, _potentialGuns.Length);
			Instantiate(_potentialGuns[selectedGun], _spawnPoint.position, Quaternion.identity);

			transform.localScale = new Vector3(1.2f, 1.2f, 1f);
		}
		if (_isOpen)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, _scaleSpeed * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_notification.SetActive(true);
			_canOpen = true;

		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_notification.SetActive(false);
			_canOpen = false;
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
