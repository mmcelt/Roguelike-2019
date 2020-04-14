using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject[] _brokenPieces;
	[SerializeField] int _maxPieces = 5;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && PlayerController.Instance.DashCounter > 0)
		{
			Destroy(gameObject);

			int piecesToDrop = Random.Range(1, _maxPieces + 1);
			for (int i=0; i<piecesToDrop; i++)
			{
				int randomPiece = Random.Range(0, _brokenPieces.Length);
				Instantiate(_brokenPieces[randomPiece], transform.position, transform.rotation);
			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
