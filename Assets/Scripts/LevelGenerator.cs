﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		//generate the starting room...
		Instantiate(_layoutRoom, _generationPoint.position, _generationPoint.rotation).GetComponent<SpriteRenderer>().color = _startColor;

		_selectedDirection = (Direction)Random.Range(0, 4);
		MoveGenerationPoint();
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
				_generationPoint.position = new Vector3(0f, _yOffset, 0f);
				break;
			case Direction.RIGHT:
				_generationPoint.position = new Vector3(_xOffset, 0f, 0f);
				break;
			case Direction.DOWN:
				_generationPoint.position = new Vector3(0f, -_yOffset, 0f);
				break;
			case Direction.LEFT:
				_generationPoint.position = new Vector3(-_xOffset, 0f, 0f);
				break;
		}
	}
	#endregion
}
