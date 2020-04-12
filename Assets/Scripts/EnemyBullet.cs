using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	#region Fields

	[SerializeField] float _speed;

	Vector3 _direction;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_direction = PlayerController.Instance.transform.position - transform.position;
		_direction.Normalize();
	}
	
	void Update() 
	{
		transform.position += _direction * _speed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{

		}

		Destroy(gameObject);
	}

	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
