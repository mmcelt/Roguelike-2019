using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _message;
	[SerializeField] PlayerController _playerToSpawn;

	bool _canSelect;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (_canSelect)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				Vector3 playerPos = PlayerController.Instance.transform.position;
				Destroy(PlayerController.Instance.gameObject);

				PlayerController newPlayer = Instantiate(_playerToSpawn, playerPos, Quaternion.identity);
				PlayerController.Instance = newPlayer;
				gameObject.SetActive(false);
				CameraController.Instance._target = newPlayer.transform;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canSelect = true;
			_message.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canSelect = false;
			_message.SetActive(false);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
