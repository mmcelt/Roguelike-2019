using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
	#region Fields

	public enum Direction { UP, RIGHT, DOWN, LEFT };

	[Header("Shop")]
	[SerializeField] bool _includeShop;
	[SerializeField] int _minDistanceToShop, _maxDistanceToShop;
	[Header("Gun Room")]
	[SerializeField] bool _includeGunRoom;
	[SerializeField] int _minDistanceToGunRoom, _maxDistanceToGunRoom;
	[Header("Layout")]
	[SerializeField] GameObject _layoutRoom;
	[SerializeField] Color _startColor, _endColor, _shopColor, _gunColor;
	[SerializeField] int _distanceToEnd;
	[SerializeField] Transform _generationPoint;
	[SerializeField] Direction _selectedDirection;
	[SerializeField] float _xOffset = 18f;
	[SerializeField] float _yOffset = 10f;
	[Header("Rooms")]
	[SerializeField] LayerMask _roomLayer;
	[SerializeField] RoomCenter _centerStart, _centerEnd, _centerShop, _centerGun;
	[SerializeField] RoomCenter[] _potentialCenters;

	GameObject _endRoom, _shopRoom, _gunRoom;

	List<GameObject> _layoutRoomObjects = new List<GameObject>();
	List<GameObject> _generatedOutlines = new List<GameObject>();

	public RoomPrefabs _rooms;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		//generate the starting room...
		Instantiate(_layoutRoom, _generationPoint.position, _generationPoint.rotation).GetComponent<SpriteRenderer>().color = _startColor;

		_selectedDirection = (Direction)Random.Range(0, 4);
		MoveGenerationPoint();

		//generate the remaining rooms...
		for (int i=0; i<_distanceToEnd; i++)
		{
			GameObject newRoom = Instantiate(_layoutRoom, _generationPoint.position, _generationPoint.rotation);

			_layoutRoomObjects.Add(newRoom);

			if (i + 1 == _distanceToEnd)  //this is the last room
			{
				newRoom.GetComponent<SpriteRenderer>().color = _endColor;
				_layoutRoomObjects.RemoveAt(_layoutRoomObjects.Count-1);
				_endRoom = newRoom;
			}

			_selectedDirection = (Direction)Random.Range(0, 4);
			MoveGenerationPoint();

			while (Physics2D.OverlapCircle(_generationPoint.position, 0.2f, _roomLayer))
			{
				//_selectedDirection = (Direction)Random.Range(0, 4);
				MoveGenerationPoint();
			}
		}
		//put in a shop...
		if (_includeShop)
		{
			int shopSelector = Random.Range(_minDistanceToShop, _maxDistanceToShop + 1);
			_shopRoom = _layoutRoomObjects[shopSelector];
			_layoutRoomObjects.RemoveAt(shopSelector);
			_shopRoom.GetComponent<SpriteRenderer>().color = _shopColor;
		}
		//put in a gun room
		if (_includeGunRoom)
		{
			int gunSelector = Random.Range(_minDistanceToGunRoom, _maxDistanceToGunRoom);
			_gunRoom = _layoutRoomObjects[gunSelector];
			_layoutRoomObjects.RemoveAt(gunSelector);
			_gunRoom.GetComponent<SpriteRenderer>().color = _gunColor;
		}
		//create room outlines...
		CreateRoomOutline(Vector3.zero);	//start room

		//in-between rooms...
		foreach(GameObject room in _layoutRoomObjects)
		{
			CreateRoomOutline(room.transform.position);
		}

		//end room
		CreateRoomOutline(_endRoom.transform.position);

		//shop room
		if (_includeShop)
			CreateRoomOutline(_shopRoom.transform.position);

		//gun room
		if (_includeGunRoom)
			CreateRoomOutline(_gunRoom.transform.position);

		//create room centers...
		foreach (GameObject outline in _generatedOutlines)
		{
			bool generateCenter = true;

			if (outline.transform.position == Vector3.zero)	//start room
			{
				Instantiate(_centerStart, outline.transform.position, Quaternion.identity)._theRoom = outline.GetComponent<Room>();
				generateCenter = false;
			}
			if (outline.transform.position == _endRoom.transform.position)	//end room
			{
				Instantiate(_centerEnd, outline.transform.position, Quaternion.identity)._theRoom = outline.GetComponent<Room>();
				generateCenter = false;
			}
			if (_includeShop)
			{
				if (outline.transform.position == _shopRoom.transform.position) //shop room
				{
					Instantiate(_centerShop, outline.transform.position, Quaternion.identity)._theRoom = outline.GetComponent<Room>();
					generateCenter = false;
				}
			}
			if (_includeGunRoom)
			{
				if (outline.transform.position == _gunRoom.transform.position) //gun room
				{
					Instantiate(_centerGun, outline.transform.position, Quaternion.identity)._theRoom = outline.GetComponent<Room>();
					generateCenter = false;
				}
			}
			if (generateCenter)	//other rooms
			{
				int selectedCenter = Random.Range(0, _potentialCenters.Length);
				Instantiate(_potentialCenters[selectedCenter], outline.transform.position, Quaternion.identity)._theRoom = outline.GetComponent<Room>();
			}
		}
	}

	void Update()
	{
#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void MoveGenerationPoint()
	{
		switch (_selectedDirection)
		{
			case Direction.UP:
				_generationPoint.position += new Vector3(0f, _yOffset, 0f);
				break;
			case Direction.RIGHT:
				_generationPoint.position += new Vector3(_xOffset, 0f, 0f);
				break;
			case Direction.DOWN:
				_generationPoint.position += new Vector3(0f, -_yOffset, 0f);
				break;
			case Direction.LEFT:
				_generationPoint.position += new Vector3(-_xOffset, 0f, 0f);
				break;
		}
	}

	void CreateRoomOutline(Vector3 roomPosition)
	{
		bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, _yOffset, 0f), 0.2f, _roomLayer);
		bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -_yOffset, 0f), 0.2f, _roomLayer);
		bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-_xOffset, 0f, 0f), 0.2f, _roomLayer);
		bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(_xOffset, 0f, 0f), 0.2f, _roomLayer);

		int directionCount = 0;
		if (roomAbove)
			directionCount++;

		if (roomBelow)
			directionCount++;

		if (roomLeft)
			directionCount++;

		if (roomRight)
			directionCount++;

		switch (directionCount)
		{
			case 0:
				Debug.LogError("No Connections detected!");
				break;
			case 1:
				if (roomAbove)
				{
					_generatedOutlines.Add(Instantiate(_rooms.singleUp, roomPosition, Quaternion.identity));
				}
				if (roomBelow)
				{
					_generatedOutlines.Add(Instantiate(_rooms.singleDown, roomPosition, Quaternion.identity));
				}
				if (roomLeft)
				{
					_generatedOutlines.Add(Instantiate(_rooms.singleLeft, roomPosition, Quaternion.identity));
				}
				if (roomRight)
				{
					_generatedOutlines.Add(Instantiate(_rooms.singleRight, roomPosition, Quaternion.identity));
				}
				break;
			case 2:
				if(roomAbove && roomBelow)
				{
					_generatedOutlines.Add(Instantiate(_rooms.doubleUpDown, roomPosition, Quaternion.identity));
				}
				if (roomAbove && roomLeft)
				{
					_generatedOutlines.Add(Instantiate(_rooms.doubleLeftUp, roomPosition, Quaternion.identity));
				}
				if (roomAbove && roomRight)
				{
					_generatedOutlines.Add(Instantiate(_rooms.doubleUpRight, roomPosition, Quaternion.identity));
				}
				if (roomLeft && roomBelow)
				{
					_generatedOutlines.Add(Instantiate(_rooms.doubleDownLeft, roomPosition, Quaternion.identity));
				}
				if (roomRight && roomBelow)
				{
					_generatedOutlines.Add(Instantiate(_rooms.doubleRightDown, roomPosition, Quaternion.identity));
				}
				if (roomLeft && roomRight)
				{
					_generatedOutlines.Add(Instantiate(_rooms.doubleLeftRight, roomPosition, Quaternion.identity));
				}
				break;
			case 3:
				if (roomAbove && roomRight && roomBelow)
				{
					_generatedOutlines.Add(Instantiate(_rooms.tripleUpRightDown, roomPosition, Quaternion.identity));
				}
				if (roomLeft && roomRight && roomBelow)
				{
					_generatedOutlines.Add(Instantiate(_rooms.tripleRightDownLeft, roomPosition, Quaternion.identity));
				}
				if (roomLeft && roomAbove && roomBelow)
				{
					_generatedOutlines.Add(Instantiate(_rooms.tripleDownLeftUp, roomPosition, Quaternion.identity));
				}
				if (roomLeft && roomRight && roomAbove)
				{
					_generatedOutlines.Add(Instantiate(_rooms.tripleLeftUpRight, roomPosition, Quaternion.identity));
				}
				break;
			case 4:
				if (roomLeft && roomRight && roomAbove && roomBelow)
				{
					_generatedOutlines.Add(Instantiate(_rooms.fourWay, roomPosition, Quaternion.identity));
				}
				break;
		}
	}
	#endregion
}

[System.Serializable]
public class RoomPrefabs
{
	public GameObject singleUp, singleDown, singleRight, singleLeft,
		doubleLeftRight, doubleUpDown, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp, tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight, fourWay;
}
