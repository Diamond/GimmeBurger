using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControllerScript : MonoBehaviour {
	public int score = 0;
	public Text scoreDisplay;
	public FoodSpawnerScript foodSpawner;
	public int difficultyIncreaseLevel = 1000;

	void Start() {
		foodSpawner = GameObject.Find("Food Spawner").GetComponent<FoodSpawnerScript>();
	}

	public void AddPoints(int points) {
		score += points;
		scoreDisplay.text = "Score: " + score.ToString();
		UpdateDifficulty();
	}

	public void UpdateDifficulty() {
		float speedUpInterval = 0.1f * (score / 1000);
		foodSpawner.foodSpeed -= speedUpInterval;
	}
}
