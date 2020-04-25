using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
	#region Fields

	public static CharacterSelectManager Instance;

	public PlayerController _activePlayer;
	public CharacterSelector _activeCharSelect;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Instance = this;
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
