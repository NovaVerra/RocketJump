using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
	/** Game Configuration */
	[SerializeField]
	Vector3	MovementVector;

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
		Vector3	Offset = MovementVector * MovementFactor;
		transform.position = StartingPosition + Offset;
	}
}
