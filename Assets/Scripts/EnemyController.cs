using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	#region Fields

	[Header("Stats")]
	[SerializeField] float _moveSpeed;
	[SerializeField] int _health = 150;
	Vector3 _moveDirection;

	[Header("Chase Player")]
	[SerializeField] bool _shouldChasePlayer;
	[SerializeField] float _rangeToChasePlayer;

	[Header("Run Away")]
	[SerializeField] bool _shouldRunAway;
	[SerializeField] float _rangeToRunAway;

	[Header("Wandering")]
	[SerializeField] bool _shouldWander;
	[SerializeField] float _wanderLength;
	[SerializeField] float _pauseLength;
	float _wanderCounter, _pauseCounter;
	Vector3 _wanderDirection;

	[Header("Patrolling")]
	[SerializeField] bool _shouldPatrol;
	[SerializeField] Transform[] _patrolPoints;
	int _currentPatrolPoint;

	[Header("Shooting")]
	[SerializeField] bool _shouldShoot;
	[SerializeField] GameObject _bullet;
	[SerializeField] Transform _firePoint;
	[SerializeField] float _fireRate;
	[SerializeField] float _rangeToShootPlayer;  //has to be >= _rangeToChasePlayer
	float _fireCounter;

	[Header("Drop Items")]
	[SerializeField] bool _shouldDropItem;
	[SerializeField] GameObject[] _itemsToDrop;
	[SerializeField] float _itemDropPercent;
	[Header("FX")]
	[SerializeField] GameObject[] _deathSplatters;
	[SerializeField] GameObject _hurtEffect;
	[SerializeField] int _hurtSFX, _deathSFX, _shootSFX;

	[Header("References")]
	[SerializeField] Rigidbody2D _theRB;
	[SerializeField] SpriteRenderer _theSprite;
	Animator _anim;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_anim = GetComponent<Animator>();

		if (_shouldWander)
			_pauseCounter = Random.Range(_pauseLength * 0.75f, _pauseLength * 1.25f);
	}
	
	void Update() 
	{
		if (_theSprite.isVisible && PlayerController.Instance.gameObject.activeInHierarchy)
		{
			_moveDirection = Vector3.zero;

			if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < _rangeToChasePlayer && _shouldChasePlayer)
			{
				_moveDirection = PlayerController.Instance.transform.position - transform.position;
			}
			//else
			//{
			//	_moveDirection = Vector3.zero;
			//	_theRB.velocity = Vector2.zero;
			//	_anim.SetBool("isMoving", false);
			//}
			else
			{
				if (_shouldWander)
				{
					if (_wanderCounter > 0)	//move..
					{
						_wanderCounter -= Time.deltaTime;

						//move the enemy...
						_moveDirection = _wanderDirection;

						if (_wanderCounter <= 0)	//pause..
						{
							_pauseCounter = Random.Range(_pauseLength * 0.75f, _pauseLength * 1.25f);
						}
					}
					if (_pauseCounter > 0)
					{
						_pauseCounter -= Time.deltaTime;

						if (_pauseCounter <= 0)	//reset wander & move...
						{
							_wanderCounter = Random.Range(_wanderLength * 0.75f, _wanderLength * 1.25f);

							_wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
						}
					}
				}

				if (_shouldPatrol)
				{
					_moveDirection = _patrolPoints[_currentPatrolPoint].position - transform.position;

					if(Vector3.Distance(transform.position, _patrolPoints[_currentPatrolPoint].position) < 0.2f)
					{
						_currentPatrolPoint++;
						if (_currentPatrolPoint >= _patrolPoints.Length)
							_currentPatrolPoint = 0;
					}
				}
			}

			if (_shouldRunAway && Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < _rangeToRunAway)
			{
				_moveDirection = transform.position - PlayerController.Instance.transform.position;
			}

			_moveDirection.Normalize();
			_theRB.velocity = _moveDirection * _moveSpeed;

			if (_shouldShoot && Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < _rangeToShootPlayer)
			{
				Shoot();
			}
		}
		else
		{
			_theRB.velocity = Vector2.zero;
		}
		//animations...
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
	#endregion
}
