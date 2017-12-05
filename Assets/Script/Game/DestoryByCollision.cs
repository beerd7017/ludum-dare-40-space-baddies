using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryByCollision : MonoBehaviour 
{
	private GameController _gameController;
	public int ScoreValue;
	public int HitPoints;

	void Start ()
	{
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
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("PROJECTILE") || other.gameObject.CompareTag("CANNONPROJECTILE"))
		{
			other.gameObject.GetComponent<ProjectileHit>().Hit();
			int damageDone = other.gameObject.GetComponent<Damage>().GetDamageValue();

			gameObject.GetComponent<DamageController>().ReduceTargetHp(damageDone);
			Destroy(other.gameObject);
		} 
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("EXPLOSION"))
		{
			int damageDone = other.gameObject.GetComponent<Damage>().GetDamageValue();
			Debug.Log("Trigger Damage cloud" + damageDone);
			gameObject.GetComponent<DamageController>().ReduceTargetHp(damageDone);
		}
	}
}
