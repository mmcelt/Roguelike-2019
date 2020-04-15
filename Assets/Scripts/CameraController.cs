using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	#region Fields

	public static CameraController Instance;

	[SerializeField] float _moveSpeed;
	[SerializeField] Transform _target;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Instance = this;
	}

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (_target == null) return;

		transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.position.x, _target.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
