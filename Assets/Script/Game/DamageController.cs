using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{

	private GameController _gameController;
	[SerializeField] public GameObject Explosion;
	public int TargetHitPoints;
	public int ScoreReward;
	public GameObject HitAudioSource;
	public GameObject DeathAudioSource;
	
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			_gameController = gameControllerObject.GetComponent<GameController>();
		}

		if (_gameController == null)
		{
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	public void ReduceTargetHp(int damage)
	{
		Instantiate(HitAudioSource, transform.position, Quaternion.identity);
		TargetHitPoints = TargetHitPoints - damage;
		if (TargetHitPoints <= 0)
		{
			TargetDeath();	
		}
	}

	public void TargetDeath()
	{
		Instantiate(DeathAudioSource, transform.position, Quaternion.identity);
		_gameController.AddScore(ScoreReward);
		Instantiate(Explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
