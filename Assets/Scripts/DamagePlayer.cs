using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{	
	#region Fields

	
	#endregion

	#region MonoBehaviour Methods

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			PlayerHealthController.Instance.DamagePlayer();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			PlayerHealthController.Instance.DamagePlayer();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
			PlayerHealthController.Instance.DamagePlayer();
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
			PlayerHealthController.Instance.DamagePlayer();
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
