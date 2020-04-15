using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	#region Fields

	public Slider _healthSlider;
	public Text _healthText;
	public GameObject _deathScreen;
	public Image _fadeScreen;
	public float _fadeSpeed;

	bool _fadeToBlack, _fadeFromBlack;

	public static UIController Instance;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Instance = this;
	}

	void Start() 
	{
		_fadeFromBlack = true;
		_fadeToBlack = false;
	}
	
	void Update() 
	{
		if (_fadeFromBlack)
		{
			_fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));

			if (_fadeScreen.color.a == 0)
				_fadeFromBlack = false;
		}
		if (_fadeToBlack)
		{
			_fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 1f, _fadeSpeed * Time.deltaTime));

			if (_fadeScreen.color.a == 1)
				_fadeToBlack = false;
		}

	}
	#endregion

	#region Public Methods

	public void FadeToBlack()
	{
		_fadeToBlack = true;
		_fadeFromBlack = false;
	}
	#endregion

	#region Private Methods


	#endregion
}
