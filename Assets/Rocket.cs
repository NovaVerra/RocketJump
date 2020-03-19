using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	Rigidbody		RB_Rocket;
	AudioSource		A_RocketRumble;
	[SerializeField] float	RcsThrust = 300f;
	[SerializeField] float	MainThrust = 300f;

	// Start is called before the first frame update
	void	Start()
	{
		RB_Rocket = GetComponent<Rigidbody>();
		A_RocketRumble = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void	Update()
	{
		ProcessInput();
	}

	void	ProcessInput()
	{
		Thrust();
		Rotate();
	}

	void	Thrust()
	{
		if (Input.GetKey(KeyCode.Space)) /** Thruster */
		{
			float	ThrustThisFrame = MainThrust * Time.deltaTime;
			print("Space is pressed");
			RB_Rocket.AddRelativeForce(Vector3.up * ThrustThisFrame);
			if (!A_RocketRumble.isPlaying)
				A_RocketRumble.Play();
		}
		else
		{
			A_RocketRumble.Stop();
		}
	}

	void	Rotate()
	{

		RB_Rocket.freezeRotation = true;
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) /** Tilt left */
		{
			float	RotationThisFrame = RcsThrust * Time.deltaTime;
			print("Left Arrow is pressed");
			transform.Rotate(Vector3.forward * RotationThisFrame);
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) /** Tilt right */
		{
			float	RotationThisFrame = RcsThrust * Time.deltaTime;
			print("Right Arrow is pressed");
			transform.Rotate(-Vector3.forward * RotationThisFrame);
		}
		RB_Rocket.freezeRotation = false;
	}
}
