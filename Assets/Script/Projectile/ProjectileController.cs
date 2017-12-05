using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	private Rigidbody2D _rigidbody;
	public Vector2 MoveSpeed;
	public float RateOfFire;
	
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.velocity = MoveSpeed;
	}
	
	void Update ()
	{
		_rigidbody.velocity = MoveSpeed;
	}

	public float GetRateOfFire()
	{
		return RateOfFire;
	}
	
}
