using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Fields

	public static PlayerController Instance;

	[SerializeField] float _moveSpeed;
	[SerializeField] Rigidbody2D _theRB;
	[SerializeField] Transform _gunHand;
	[SerializeField] Animator _anim;
	[SerializeField] GameObject _bulletPrefab;
	[SerializeField] Transform _firePoint;
	[SerializeField] float _timeBetweenShots;

	Vector2 _moveInput;
	Camera _theCam;
	float _shotCounter;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Instance = this;
	}

	void Start() 
	{
		_theCam = Camera.main;
	}
	
	void Update() 
	{
		_moveInput.x = Input.GetAxisRaw("Horizontal");
		_moveInput.y = Input.GetAxisRaw("Vertical");


		_moveInput.Normalize();	//keeps diagonal speed the same as right angle speed

		//transform.position += new Vector3(_moveInput.x * _moveSpeed * Time.deltaTime, _moveInput.y * _moveSpeed * Time.deltaTime, 0f);

		_theRB.velocity = _moveInput * _moveSpeed;

		Vector3 mousePos = Input.mousePosition;
		Vector3 screenPoint = _theCam.WorldToScreenPoint(transform.localPosition);

		//flip the player & square away the gun...
		if (mousePos.x < screenPoint.x)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
			_gunHand.localScale = new Vector3(-1f, -1f, 1f);
		}
		else
		{
			transform.localScale = Vector3.one;
			_gunHand.localScale = Vector3.one;
		}

		//rotate the gun arm...
		Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
		float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
		_gunHand.rotation = Quaternion.Euler(0f, 0f, angle);

		//fire bullet...
		if (Input.GetMouseButtonDown(0))
		{
			Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
			_shotCounter = _timeBetweenShots;
		}

		if (Input.GetMouseButton(0))
		{
			_shotCounter -= Time.deltaTime;

			if (_shotCounter <= 0)
			{
				Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
				_shotCounter = _timeBetweenShots;
			}
		}

		//trigger the animations..
		if(_moveInput != Vector2.zero)
		{
			_anim.SetBool("isMoving", true);
		}
		else
		{
			_anim.SetBool("isMoving", false);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
