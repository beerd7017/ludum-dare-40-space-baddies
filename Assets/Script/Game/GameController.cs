using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public Text ScoreText;
	public Text HitPointText;
	public Text AltShotText;
	private int _score;
	public int PlayerHitPoints;
	public int AltShotCount;
	public Transform PlayerSpawnPoints;

	void Start ()
	{
		PlayerHitPoints = 100;
		AltShotCount = 0;
		UpdateScore();
		UpdatePlayerHitPoints();
		UpdateAltShotCount();
	}
	
	public void AddScore(int pointsReceived)
	{
		_score += pointsReceived;
		UpdateScore();
	}

	private void ResetScore()
	{
		_score = 0;
		UpdateScore();
	}

	void UpdateScore()
	{
		ScoreText.text = "Score: " + _score;
	}

	public void AddPlayerHitPoints(int addPoints)
	{
		if (PlayerHitPoints < 200)
		{
			PlayerHitPoints = PlayerHitPoints + addPoints;
			if (PlayerHitPoints > 200)
			{
				PlayerHitPoints = 200;
			}
		}
		UpdatePlayerHitPoints();
	}

	public void RemovePlayerHitPoints(int removePoints)
	{
		PlayerHitPoints -= removePoints;
		UpdatePlayerHitPoints();
	}

	private void ResetPlayerHitPoints()
	{
		PlayerHitPoints = 100;
		UpdatePlayerHitPoints();
	}

	void UpdatePlayerHitPoints()
	{
		HitPointText.text = "HP: " + PlayerHitPoints;
	}

	public int GetPlayerHitPoints()
	{
		return PlayerHitPoints;
	}

	public void AddAltshots(int addShots)
	{
		if (AltShotCount < 25)
		{
			AltShotCount = AltShotCount +addShots;
			if (AltShotCount > 25)
			{
				AltShotCount = 25;
			}
		}
		UpdateAltShotCount();
	}

	public void DecrementAltShots()
	{
		AltShotCount--;
		UpdateAltShotCount();
	}

	private void ResetAltShots()
	{
		AltShotCount = 0;
		UpdateAltShotCount();
	}

	public int GetAltShotCount()
	{
		return AltShotCount;
	}
	
	void UpdateAltShotCount()
	{
		AltShotText.text = "Alt Shots Remaining: " + AltShotCount;
	}

	public void SpawnPlayer(GameObject Player)
	{
		ResetPlayerHitPoints();
		ResetScore();
		ResetAltShots();
		Debug.Log("Spawning Player: " + Player.name);
		Player.transform.position = PlayerSpawnPoints.transform.position;
		Player.transform.rotation = PlayerSpawnPoints.transform.rotation;
	}
}