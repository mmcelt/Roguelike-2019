using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
	#region Fields

	[SerializeField] bool _openWhenEnemiesCleared;
	[SerializeField] List<GameObject> _enemiesInRoom = new List<GameObject>();
	public Room _theRoom;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if (_openWhenEnemiesCleared)
			_theRoom._closeWhenEntered = true;
	}
	
	void Update() 
	{
		if (_enemiesInRoom.Count > 0 && _theRoom._roomActive && _openWhenEnemiesCleared)
			CheckEnemiesInRoom();
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void CheckEnemiesInRoom()
	{
		for (int i = 0; i < _enemiesInRoom.Count; i++)
		{
			if (_enemiesInRoom[i] == null)
			{
				_enemiesInRoom.RemoveAt(i);
				i--;
			}
		}

		if (_enemiesInRoom.Count == 0)
		{
			_theRoom.OpenDoors();
		}
	}

	#endregion
}
