using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPiece : MonoBehaviour
{
	#region Fields

	[SerializeField] float _moveSpeed = 3f;
	[SerializeField] float _deceleration = 5f;
	[SerializeField] float _lifeTime = 3f;
	[SerializeField] SpriteRenderer _theSprite;
	[SerializeField] float _fadeSpeed = 2.5f;

	Vector3 _moveDirection;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_moveDirection.x = Random.Range(-_moveSpeed, _moveSpeed);
		_moveDirection.y = Random.Range(-_moveSpeed, _moveSpeed);
		_moveDirection.z = 0f;
	}

	void Update() 
	{
		transform.position += _moveDirection * Time.deltaTime;

		_moveDirection = Vector3.Lerp(_moveDirection, Vector3.zero, _deceleration * Time.deltaTime);

		_lifeTime -= Time.deltaTime;

		if (_lifeTime <= 0)
		{
			_theSprite.color = new Color(_theSprite.color.r, _theSprite.color.g, _theSprite.color.b, Mathf.MoveTowards(_theSprite.color.a, 0f, _fadeSpeed * Time.deltaTime));

			if (_theSprite.color.a == 0f)
				Destroy(gameObject);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
