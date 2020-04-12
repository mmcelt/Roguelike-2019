using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	#region Fields

	[SerializeField] Rigidbody2D _theRB;
	[SerializeField] float _moveSpeed;
	[SerializeField] float _rangeToChasePlayer;

	Vector3 _moveDirection;
	Animator _anim;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_anim = GetComponent<Animator>();
	}
	
	void Update() 
	{
		if(Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < _rangeToChasePlayer)
		{
			_moveDirection = PlayerController.Instance.transform.position - transform.position;
		}
		else
		{
			_moveDirection = Vector3.zero;
		}

		_moveDirection.Normalize();
		_theRB.velocity = _moveDirection * _moveSpeed;

		if (_moveDirection != Vector3.zero)
			_anim.SetBool("isMoving", true);
		else
			_anim.SetBool("isMoving", false);

	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
