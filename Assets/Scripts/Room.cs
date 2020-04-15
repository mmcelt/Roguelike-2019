using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	#region Fields

	[SerializeField] bool _closeWhenEntered;
	[SerializeField] GameObject[] _doors;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			CameraController.Instance.ChangeTarget(transform);

			if (_closeWhenEntered)
			{
				foreach(GameObject door in _doors)
				{
					door.SetActive(true);
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
