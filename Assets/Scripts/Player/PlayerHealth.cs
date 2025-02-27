using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private InputReaderSO _inputReader;
	[Space(5)]

	// Variables
	[SerializeField] private float _maxHealth;
	[SerializeField] internal float _currentHealth;
	[SerializeField] GameObject _player;
	[SerializeField] Transform _chargeBarTransform; // Reference to the scale of the bar
	[SerializeField] float _chargeSpeed = 0.005f; // The rate at which bar depletes or charges


	/* Taking Damage Effect */
	private TakingDamage _takingDamageScript;

	/* Health System */
	[SerializeField] internal float _takenDamage = 1f;

	private Rigidbody2D _rb2D;

    //Functions
    private void Awake()
    {
		if (_inputReader == null)
			_inputReader = Resources.Load("ScriptableObjects/InputReader") as InputReaderSO;
    }

	//private void OnEnable()
	//{
	//  _inputReader.OnDashInput += ReduceHP;
	//}

	//private void OnDisable()
	//{
	//  _inputReader.OnDashInput -= ReduceHP;
	//}

	void Start()
	{
		_rb2D = GetComponent<Rigidbody2D>();
		_currentHealth = _maxHealth;
		_takingDamageScript = GetComponent<TakingDamage>();
	}

	void Update()
	{
		ReduceHP();
	}

	internal void GetDamage()
	{
		_currentHealth = _currentHealth - _takenDamage;
		if (_takingDamageScript != null)
		{
			_takingDamageScript.FlashOnce();
		}
	}
	void ReduceHP()
	{
		if (_player != null)
		{
			// If the player hold down Sandevistan his health bar will start to get depleted
			if (Input.GetKey(KeyCode.Space))
			{
				_currentHealth -= 0.5f;
				ReduceCharge();
			}

			// Ensure _currentHealth does not go below 0
			_currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);


			//Testing whether the player heals. Regenerate 1 HP per frame
			if (!Input.GetKey(KeyCode.Space) && _currentHealth != _maxHealth)
			{
				_currentHealth += 0.1f;
				RegenCharge();
			}

			_currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

		}
	}

	void ReduceCharge()
	{
		if (_chargeBarTransform != null)
		{
			// Calculate the proportional health scale
			float healthScale = _currentHealth / _maxHealth;

			// Calculate the scaled health bar value based on the proportional scale
			float scaledHealthBar = healthScale * 0.16f;

			// Clamp the scaled health bar value between 0 and 0.16
			float clampedScale = Mathf.Clamp(scaledHealthBar, 0f, 0.16f);

			// Update health bar scale based on clamped value
			_chargeBarTransform.localScale = new Vector3(_chargeBarTransform.localScale.x, clampedScale, _chargeBarTransform.localScale.z);
		}
	}

	void RegenCharge()
	{
		if (_chargeBarTransform != null)
		{
			// Calculate the proportional health scale
			float healthScale = _currentHealth / _maxHealth;

			// Calculate the scaled health bar value based on the proportional scale
			float scaledHealthBar = healthScale * 0.16f;

			// Clamp the scaled health bar value between 0 and 0.16
			float clampedScale = Mathf.Clamp(scaledHealthBar, 0f, 0.16f);

			// Update health bar scale based on clamped value
			_chargeBarTransform.localScale = new Vector3(_chargeBarTransform.localScale.x, clampedScale, _chargeBarTransform.localScale.z);
		}
	}

}
