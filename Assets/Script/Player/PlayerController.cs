using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private GameController _gameController;
	private Transform _firePosition;
	private GameObject _currentProjectile;
	public GameObject Player;
	public GameObject Projectile;
	public GameObject CannonProjectile;
	public GameObject ProjectileSfxAudioSource;
	public GameObject CannonProjectileSfxAudioSource;
	public GameObject PowerUpAudioSource;
	public float MoveSpeed;

	
	void Start ()
	{
		_currentProjectile = Projectile;
		_firePosition = transform.Find("firePos");
		
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
	
	void Update ()
	{
		transform.Translate(MoveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, MoveSpeed*Input.GetAxis("Vertical")*Time.deltaTime, 0f);
		
		if (Input.GetKeyDown(KeyCode.J))
		{
			StartCoroutine("Fire");
		}
		else if (Input.GetKeyUp(KeyCode.J))
		{
			StopCoroutine("Fire");
		}

		if(Input.GetKeyDown(KeyCode.K))
		{
			StartCoroutine("AltFire");
		} else if (Input.GetKeyUp(KeyCode.K))
		{
			StopCoroutine("AltFire");
		}
	}

	IEnumerator Fire()
	{
		while (true)
		{
			GameObject newBullet = Instantiate(Projectile, _firePosition.position, Quaternion.identity);
			Instantiate(ProjectileSfxAudioSource, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(1f / newBullet.GetComponent<ProjectileController>().GetRateOfFire());
		}
	}

	IEnumerator AltFire()
	{
		while (_gameController.GetAltShotCount() > 0)
		{
			GameObject altBullet = Instantiate(_currentProjectile, _firePosition.position, Quaternion.identity);
			Instantiate(CannonProjectileSfxAudioSource, transform.position, Quaternion.identity);
			_gameController.DecrementAltShots();
			yield return new WaitForSeconds(1f/ altBullet.GetComponent<ProjectileController>().GetRateOfFire());
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("ENEMY"))
		{
			Destroy(other.gameObject);
			_gameController.RemovePlayerHitPoints(other.gameObject.GetComponent<Damage>().GetDamageValue());
			if (_gameController.GetPlayerHitPoints() <= 0)
			{
				_gameController.SpawnPlayer(Player);
			}
		}

		if (other.gameObject.CompareTag("HEALTH"))
		{
			Instantiate(PowerUpAudioSource, transform.position, Quaternion.identity);
			_gameController.AddPlayerHitPoints(other.gameObject.GetComponent<HealthCollision>().GetHealthValue());
			Destroy(other.gameObject);
		}

		if (other.gameObject.CompareTag("POWERUP"))
		{
			Instantiate(PowerUpAudioSource, transform.position, Quaternion.identity);
			_currentProjectile = CannonProjectile;
			_gameController.AddAltshots(5);
			Destroy(other.gameObject);
		}
	}
}