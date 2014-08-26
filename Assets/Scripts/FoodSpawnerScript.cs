using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodSpawnerScript : MonoBehaviour {
	public float           lastSpawn     = 0.0f;
	public float           spawnInterval = 3.0f;
	public Transform       foodPrefab;
	public List<Transform> ingredients;
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawn >= spawnInterval) {
			lastSpawn = Time.time;
			var food = Instantiate(foodPrefab) as Transform;
			food.GetComponent<IngredientScript>().Spawn ();
			food.GetComponent<IngredientScript>().parentScript = this;
			food.parent = this.transform;
			ingredients.Add(food);
		}
	}

	public void ResetIngredients() {
		for (int i = 0; i < ingredients.Count; i++) {
			if (ingredients[i] == null) {
				ingredients.RemoveAt(i);
			}
		}
		foreach (Transform ingredient in ingredients) {
			ingredient.GetComponent<IngredientScript>().neighbors.Clear();
		}
	}
}
