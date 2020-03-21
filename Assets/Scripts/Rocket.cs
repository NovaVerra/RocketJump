using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	/** Game Configurations */
	Rigidbody	RB_Rocket;
	AudioSource	S_Sound;

	[SerializeField] float			LevelLoadDelay = 2f;
	[SerializeField] float			RcsThrust = 300f;
	[SerializeField] float			MainThrust = 300f;

	[SerializeField] AudioClip		S_EngineThrust;
	[SerializeField] AudioClip		S_OnDeath;
	[SerializeField] AudioClip		S_OnWin;

	[SerializeField] ParticleSystem	PS_EngineThrust;
	[SerializeField] ParticleSystem	PS_OnCollision;
	[SerializeField] ParticleSystem	PS_OnWin;

	/** Game State */
	enum					GameState { Alive, Dying, Transcending };
	GameState				State = GameState.Alive;
	int						Level = 0;

	// Start is called before the first frame update
	void	Start()
	{
		RB_Rocket = GetComponent<Rigidbody>();
		S_Sound = GetComponent<AudioSource>();
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
			S_Sound.Stop();
			PS_EngineThrust.Stop();
		}
	}

	void	ApplyThrust()
	{
		float	ThrustThisFrame = MainThrust * Time.deltaTime;
		RB_Rocket.AddRelativeForce(Vector3.up * ThrustThisFrame);
		if (!S_Sound.isPlaying)
		{
			S_Sound.PlayOneShot(S_EngineThrust);
		}
		PS_EngineThrust.Play();
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
		S_Sound.Stop();
		S_Sound.PlayOneShot(S_OnWin);
		PS_OnWin.Play();
		Invoke("LoadNextLevel", LevelLoadDelay);
	}

	void	StartDeathSequence()
	{
		State = GameState.Dying;
		S_Sound.Stop();
		S_Sound.PlayOneShot(S_OnDeath);
		PS_OnCollision.Play();
		Invoke("LoadFirstLevel", LevelLoadDelay);
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
