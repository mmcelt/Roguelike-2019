using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
	#region Fields

	public enum Direction { UP, RIGHT, DOWN, LEFT };

	[SerializeField] GameObject _layoutRoom;
	[SerializeField] int _distanceToEnd;
	[SerializeField] Color _startColor, _endColor;
	[SerializeField] Transform _generationPoint;
	[SerializeField] Direction _selectedDirection;
	[SerializeField] float _xOffset = 18f;
	[SerializeField] float _yOffset = 10f;
	[SerializeField] LayerMask _roomLayer;

	GameObject _endRoom;

	List<GameObject> _layoutRoomObjects = new List<GameObject>();

	public RoomPrefabs _rooms;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		//generate the starting room...
		Instantiate(_layoutRoom, _generationPoint.position, _generationPoint.rotation).GetComponent<SpriteRenderer>().color = _startColor;

		_selectedDirection = (Direction)Random.Range(0, 4);
		MoveGenerationPoint();

		for (int i=0; i<_distanceToEnd; i++)
		{
			GameObject newRoom = Instantiate(_layoutRoom, _generationPoint.position, _generationPoint.rotation);

			_layoutRoomObjects.Add(newRoom);
			_selectedDirection = (Direction)Random.Range(0, 4);
			MoveGenerationPoint();

			while (Physics2D.OverlapCircle(_generationPoint.position, 0.2f, _roomLayer))
			{
				//_selectedDirection = (Direction)Random.Range(0, 4);
				MoveGenerationPoint();
			}

			if (i == _distanceToEnd - 1)	//this is the last room
			{
				newRoom.GetComponent<SpriteRenderer>().color = _endColor;
				_layoutRoomObjects.RemoveAt(i);
				_endRoom = newRoom;
			}
		}
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
	#endregion
}

[System.Serializable]
public class RoomPrefabs
{
	public GameObject singleUp, singleDown, singleright, singleLeft,
		doubleLeftRight, doubleUpDown, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp, tripleUpRightDown, tripleRightDownLeft, tripleDownleftUp, tripleLeftUpRight, fourWay;
}
