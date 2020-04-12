using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	#region Fields

	[SerializeField] Rigidbody2D _theRB;
	[SerializeField] float _moveSpeed;
	[SerializeField] float _rangeToChasePlayer;
	[SerializeField] int _health = 150;
	[SerializeField] GameObject[] _deathSplatters;

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

	public void DamageEnemy(int damage)
	{
		_health -= damage;

		if (_health <= 0)
		{
			_health = 0;
			Die();
		}
	}
	#endregion

	#region Private Methods

	void Die()
	{
		int selectedFX = Random.Range(0, _deathSplatters.Length);
		int rotation = Random.Range(0, 4);
		Instantiate(_deathSplatters[selectedFX], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));

		Destroy(gameObject);
	}
	#endregion
}
