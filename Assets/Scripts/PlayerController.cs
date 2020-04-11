using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Fields

	[SerializeField] float _moveSpeed;

	Vector2 _moveInput;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		_moveInput.x = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
		_moveInput.y = Input.GetAxisRaw("Vertical") * _moveSpeed * Time.deltaTime;

		transform.position += new Vector3(_moveInput.x, _moveInput.y, 0f);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
