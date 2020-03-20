using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	Rigidbody				RB_Rocket;
	AudioSource				A_RocketRumble;
	[SerializeField] float	RcsThrust = 300f;
	[SerializeField] float	MainThrust = 300f;

	enum					GameState { Alive, Dying, Transcending };
	GameState				State = GameState.Alive;
	int						Level = 0;

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
		if (State == GameState.Alive)
		{
			Thrust();
			Rotate();
		}
	}

	void	Thrust()
	{
		if (Input.GetKey(KeyCode.Space)) /** Thruster */
		{
			float	ThrustThisFrame = MainThrust * Time.deltaTime;
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
			transform.Rotate(Vector3.forward * RotationThisFrame);
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) /** Tilt right */
		{
			float	RotationThisFrame = RcsThrust * Time.deltaTime;
			transform.Rotate(-Vector3.forward * RotationThisFrame);
		}
		RB_Rocket.freezeRotation = false;
	}

	void	OnCollisionEnter(Collision CollisionEvent)
	{
		if (State != GameState.Alive) { return; }
		switch (CollisionEvent.gameObject.tag)
		{
			case "Friendly":
				// do nothing
				break;
			case "Win":
				State = GameState.Transcending;
				Invoke("LoadNextLevel", 1f);
				break;
			default:
				State = GameState.Dying;
				Invoke("LoadFirstLevel", 1f);
				break;
		}
	}

	void	LoadNextLevel()
	{
		SceneManager.LoadScene(1);
		State = GameState.Alive;
	}

	void	LoadFirstLevel()
	{
		SceneManager.LoadScene(0);
		State = GameState.Alive;
	}
}
