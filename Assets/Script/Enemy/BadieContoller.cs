using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadieContoller : MonoBehaviour
{
	private GameController _gameController;
	private Transform _playerTransform;
	public float MoveSpeed;

	void Start ()
	{
		GameObject playerObject = GameObject.FindWithTag("Player");
		_playerTransform = playerObject.transform;
	}
	
	void Update ()
	{
		//JustMoveLeft();
		ChasePlayer();
	}

	/*
	 * My Orginal AI
	 */
	void ChasePlayer()
	{
		Vector3 targetDirection = _playerTransform.position - transform.position;
		float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
		transform.Translate(Vector3.up * Time.deltaTime * MoveSpeed);
	}

	void JustMoveLeft()
	{
		transform.Translate(Vector2.left * Time.deltaTime * MoveSpeed);
	}
}