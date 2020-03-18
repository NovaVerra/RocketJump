using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	Rigidbody	RB_Rocket;
	AudioSource	A_RocketRumble;
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
		if (Input.GetKey(KeyCode.Space)) /** Thruster */
		{
			print("Space is pressed");
			RB_Rocket.AddRelativeForce(Vector3.up);
			if (!A_RocketRumble.isPlaying)
				A_RocketRumble.Play();
		}
		else
		{
			A_RocketRumble.Stop();
		}
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) /** Tilt left */
		{
			print("Left Arrow is pressed");
			transform.Rotate(Vector3.forward);
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) /** Tilt right */
		{
			print("Right Arrow is pressed");
			transform.Rotate(-Vector3.forward);
		}
		else
		{
			;
		}
	}
}
