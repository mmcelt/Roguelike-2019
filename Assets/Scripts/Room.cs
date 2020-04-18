using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	#region Fields

	[SerializeField] bool _closeWhenEntered;//, _openWhenEnemiesCleared;
	[SerializeField] GameObject[] _doors;
	//[SerializeField] List<GameObject> _enemiesInRoom = new List<GameObject>();

	[HideInInspector] public bool _roomActive;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	//void Update() 
	//{
	//	//if(_enemiesInRoom.Count > 0 && _roomActive && _openWhenEnemiesCleared)
	//	//	CheckEnemiesInRoom();
	//}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			CameraController.Instance.ChangeTarget(transform);

			if (_closeWhenEntered)
			{
				CloseDoors();
			}

			_roomActive = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			_roomActive = false;
	}
	#endregion

	#region Public Methods

	public void OpenDoors()
	{
		foreach (GameObject door in _doors)
		{
			door.SetActive(false);
		}
	}
	#endregion

	#region Private Methods

	//void CheckEnemiesInRoom()
	//{
	//	for(int i=0; i<_enemiesInRoom.Count; i++)
	//	{
	//		if (_enemiesInRoom[i] == null)
	//		{
	//			_enemiesInRoom.RemoveAt(i);
	//			i--;
	//		}
	//	}

	//	if (_enemiesInRoom.Count == 0)
	//	{
	//		OpenDoors();
	//		_closeWhenEntered = false;
	//	}
	//}

	void CloseDoors()
	{
		foreach (GameObject door in _doors)
		{
			door.SetActive(true);
		}
	}
	#endregion
}
