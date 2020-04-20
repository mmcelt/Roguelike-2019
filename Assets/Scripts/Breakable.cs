using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject[] _brokenPieces;
	[SerializeField] int _maxPieces = 5;
	[SerializeField] bool _shouldDropItem;
	[SerializeField] GameObject[] _itemsToDrop;
	[SerializeField] float _itemDropPercent;
	[SerializeField] int _breakSFX;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((other.CompareTag("Player") && PlayerController.Instance.DashCounter > 0) || (other.CompareTag("Player Bullet")))
		{
			Destroy(gameObject);

			AudioManager.Instance.PlaySFX(_breakSFX);

			//show broken pieces
			int boxPieces = Random.Range(1, _maxPieces + 1);
			for (int i=0; i<boxPieces; i++)
			{
				int randomPiece = Random.Range(0, _brokenPieces.Length);
				Instantiate(_brokenPieces[randomPiece], transform.position, transform.rotation);
			}

			//drop items
			if (_shouldDropItem)
			{
				float dropChance = Random.Range(0f, 100f);
				if (dropChance <= _itemDropPercent)
				{
					int randomItem = Random.Range(0, _itemsToDrop.Length);
					Instantiate(_itemsToDrop[randomItem], transform.position, Quaternion.identity);
				}
			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
