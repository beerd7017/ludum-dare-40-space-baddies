using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonExplosion : MonoBehaviour
{
	[SerializeField] private float _explosionDuration;
	[SerializeField] private GameObject _explosionObject;
	public GameObject ExplosionAudioSource;
	
	void Start () {
		Invoke("Explode", _explosionDuration);
	}
	
	void Explode()
	{
		Instantiate(ExplosionAudioSource, transform.position, Quaternion.identity);
		Instantiate(_explosionObject, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
