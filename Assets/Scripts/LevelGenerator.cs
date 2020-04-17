using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _layoutRoom;
	[SerializeField] int _distanceToEnd;
	[SerializeField] Color _startColor, _endColor;
	[SerializeField] Transform _generationPoint;
	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		//generate the starting room...
		Instantiate(_layoutRoom, _generationPoint.position, _generationPoint.rotation).GetComponent<SpriteRenderer>().color = _startColor;
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
