using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
	[SerializeField] public GameObject HitObject;

	private const float MaxDuration = 4f;

	void Start ()
	{
		Invoke("DestoryMissedProjectiles", MaxDuration);
	}

	public void Hit()
	{
		Instantiate(HitObject, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	private void DestoryMissedProjectiles()
	{
		Destroy(gameObject);
	}
}
