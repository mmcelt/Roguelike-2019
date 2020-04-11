using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Fields

	[SerializeField] float _moveSpeed;

	Vector2 _moveInput;

	[SerializeField] Rigidbody2D _theRB;
	[SerializeField] Transform _gunHand;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		_moveInput.x = Input.GetAxisRaw("Horizontal");
		_moveInput.y = Input.GetAxisRaw("Vertical");

		//transform.position += new Vector3(_moveInput.x * _moveSpeed * Time.deltaTime, _moveInput.y * _moveSpeed * Time.deltaTime, 0f);

		_theRB.velocity = _moveInput * _moveSpeed;

		Vector3 mousePos = Input.mousePosition;
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

		//rotate the gun arm
		Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
		float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
		_gunHand.rotation = Quaternion.Euler(0f, 0f, angle);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
