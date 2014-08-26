using UnityEngine;
using System.Collections;

public class FoodSpawnerScript : MonoBehaviour {
	public float     lastSpawn = 0.0f;
	public float     spawnInterval = 3.0f;
	public Transform foodPrefab;
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawn >= spawnInterval) {
			lastSpawn = Time.time;
			var food = Instantiate(foodPrefab) as Transform;
			food.GetComponent<IngredientScript>().Spawn ();
		}
	}
}
