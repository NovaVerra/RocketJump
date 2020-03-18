using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	Rigidbody	RB_Rocket;
	// Start is called before the first frame update
	void	Start()
	{
		RB_Rocket = GetComponent<Rigidbody>();
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
		}
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) /** Tilt left */
		{
			print("Left Arrow is pressed");
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) /** Tilt right */
		{
			print("Right Arrow is pressed");
		}
		else
		{
			;
		}
	}
}
