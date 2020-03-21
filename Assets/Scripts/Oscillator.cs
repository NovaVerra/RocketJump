using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
	/** Game Configuration */
	[SerializeField]
	Vector3	MovementVector = new Vector3(10f, 10f, 10f);
	[SerializeField]
	float	Period = 0.5f;

	[Range(0, 1)] [SerializeField]
	float	MovementFactor;

	Vector3	StartingPosition;

	// Start is called before the first frame update
	void Start()
	{
		StartingPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (Period <= Mathf.Epsilon)
		{
			Debug.LogError("Cannot divide by 0");
			return ;
		}
		float		Cycle = Time.time * Period;				// frame rate independent
		const float	Tau = Mathf.PI * 2;
		float		RawSinWave = Mathf.Sin(Cycle * Tau);	// goes from -1 to +1
		MovementFactor = RawSinWave / 2f + 0.5f;			// goes from 0 to 1
		Vector3	Offset = MovementVector * MovementFactor;
		transform.position = StartingPosition + Offset;
	}
}
