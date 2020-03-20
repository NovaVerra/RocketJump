using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{

	/** Game Configurations */
	Rigidbody	RB_Rocket;
	AudioSource	A_Audio;
	[SerializeField] float		RcsThrust = 300f;
	[SerializeField] float		MainThrust = 300f;
	[SerializeField] AudioClip	EngineThrust;
	[SerializeField] AudioClip	OnDeath;
	[SerializeField] AudioClip	OnWin;

	/** Game State */
	enum					GameState { Alive, Dying, Transcending };
	GameState				State = GameState.Alive;
	int						Level = 0;

	// Start is called before the first frame update
	void	Start()
	{
		RB_Rocket = GetComponent<Rigidbody>();
		A_Audio = GetComponent<AudioSource>();
		RB_Rocket.constraints = RigidbodyConstraints.FreezePositionZ;
		RB_Rocket.constraints = RigidbodyConstraints.FreezeRotationX;
		RB_Rocket.constraints = RigidbodyConstraints.FreezeRotationY;
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
			RespondToThrustInput();
			RespondToRotateInput();
		}
	}

	void	RespondToThrustInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			ApplyThrust();
		}
		else
		{
			A_Audio.Stop();
		}
	}

	void	ApplyThrust()
	{
		float	ThrustThisFrame = MainThrust * Time.deltaTime;
		RB_Rocket.AddRelativeForce(Vector3.up * ThrustThisFrame);
		if (!A_Audio.isPlaying)
			A_Audio.PlayOneShot(EngineThrust);
	}

	void	RespondToRotateInput()
	{
		RB_Rocket.freezeRotation = true;
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			float	RotationThisFrame = RcsThrust * Time.deltaTime;
			transform.Rotate(Vector3.forward * RotationThisFrame);
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
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
				StartWinSequence();
				break;
			default:
				StartDeathSequence();
				break;
		}
	}

	void	StartWinSequence()
	{
		State = GameState.Transcending;
		A_Audio.Stop();
		A_Audio.PlayOneShot(OnWin);
		Invoke("LoadNextLevel", 1f);
	}

	void	StartDeathSequence()
	{
		State = GameState.Dying;
		A_Audio.Stop();
		A_Audio.PlayOneShot(OnDeath);
		Invoke("LoadFirstLevel", 1f);
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
