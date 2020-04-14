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
	[SerializeField] GameObject _hurtEffect;
	[SerializeField] SpriteRenderer _theSprite;
	[SerializeField] int _hurtSFX, _deathSFX, _shootSFX;

	[Header("Shooting")]
	[SerializeField] bool _shouldShoot;
	[SerializeField] GameObject _bullet;
	[SerializeField] Transform _firePoint;
	[SerializeField] float _fireRate;
	[SerializeField] float _rangeToShootPlayer;	//has to be >= _rangeToChasePlayer

	float _fireCounter;

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
		if (_theSprite.isVisible && PlayerController.Instance.gameObject.activeInHierarchy)
		{
			Movement();
			Shoot();
		}
		else
		{
			_theRB.velocity = Vector2.zero;
			_anim.SetBool("isMoving", false);
		}
	}
	#endregion

	#region Public Methods

	public void DamageEnemy(int damage)
	{
		_health -= damage;

		Instantiate(_hurtEffect, transform.position, Quaternion.identity);
		AudioManager.Instance.PlaySFX(_hurtSFX);

		if (_health <= 0)
		{
			_health = 0;
			Die();
		}
	}
	#endregion

	#region Private Methods

	void Movement()
	{
		if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < _rangeToChasePlayer)
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

	void Shoot()
	{
		if (_shouldShoot && Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < _rangeToShootPlayer)
		{
			_fireCounter -= Time.deltaTime;
			if (_fireCounter <= 0)
			{
				_fireCounter = _fireRate;
				Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
				AudioManager.Instance.PlaySFX(_shootSFX);
			}
		}
	}

	void Die()
	{
		int selectedFX = Random.Range(0, _deathSplatters.Length);
		int rotation = Random.Range(0, 4);
		Instantiate(_deathSplatters[selectedFX], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));
		AudioManager.Instance.PlaySFX(_deathSFX);

		Destroy(gameObject);
	}
	#endregion
}
