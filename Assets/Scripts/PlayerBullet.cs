using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	#region Fields

	[SerializeField] float _speed = 7.5f;
	[SerializeField] Rigidbody2D _theRB;
	[SerializeField] GameObject _impactFX;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		_theRB.velocity = transform.right * _speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Vector3 bulletOffset = GetComponent<BoxCollider2D>().size.x / 2 * transform.right;

		Instantiate(_impactFX, transform.position + bulletOffset, Quaternion.identity);
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
