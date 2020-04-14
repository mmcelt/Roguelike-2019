using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
	#region Fields

	SpriteRenderer _theSprite;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_theSprite = GetComponent<SpriteRenderer>();
		_theSprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f);
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
