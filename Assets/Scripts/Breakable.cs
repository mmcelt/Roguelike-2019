using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{	
	#region Fields

	
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
		if (other.CompareTag("Player") && PlayerController.Instance.DashCounter > 0)
		{
			Destroy(gameObject);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
