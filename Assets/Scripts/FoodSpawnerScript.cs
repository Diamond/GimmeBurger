using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodSpawnerScript : MonoBehaviour {
	public float           lastSpawn     = 0.0f;
	public float           spawnInterval = 3.0f;
	public Transform       foodPrefab;
	public List<Transform> ingredients;
	public List<Transform> addedBuns;
	public int             bunCount = 0;
	public GameControllerScript gcScript;
	public float		   foodSpeed = 3.0f;

	void Start() {
		gcScript = GameObject.Find ("GameController").GetComponent<GameControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawn >= spawnInterval) {
			lastSpawn = Time.time;
			var food = Instantiate(foodPrefab) as Transform;
			IngredientScript iscript = food.GetComponent<IngredientScript>();
			iscript.Spawn ();
			iscript.parentScript = this;
			iscript.speed        = foodSpeed;
			food.parent          = this.transform;
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

	public void AddBun(IngredientScript bun) {
		addedBuns.Add(bun.transform);
		Debug.Log ("Added bun!");
		bunCount++;
		if (bunCount % 2 == 0) {
			int start = -1;
			int end   = -1;
			for (int i = 0; i < ingredients.Count; i++) {
				IngredientScript iscript = ingredients[i].GetComponent<IngredientScript>();
				if (iscript.type == 0 && iscript.placed && !iscript.destroyed) {
					if (start == -1) {
						start = i;
					} else if (end == -1) {
						end = i;
					}
				}
			}
			for (int i = start; i <= end; i++) {
				var ingredient = ingredients[i];
				ingredient.GetComponent<IngredientScript>().Remove ();
			}
			addedBuns.Clear();
			gcScript.AddPoints((end - start) * 100);
		}
	}
}
