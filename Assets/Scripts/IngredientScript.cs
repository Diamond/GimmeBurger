using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientScript : MonoBehaviour {
	public float speed = 1.0f;
    public bool placed = false;
	public bool destroyed = false;
	public int type = 0;
	public List<Material> colors;
	public List<Transform> neighbors;
	public FoodSpawnerScript parentScript;

	// Use this for initialization
	void Start () {
		Input.simulateMouseWithTouches = true;
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!placed && !destroyed) {
			this.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
		}

		if (placed && !destroyed) {
			if (type == 0 && neighbors.Count > 0 && !parentScript.addedBuns.Contains(this.transform)) {
				parentScript.AddBun(this);
			}
			int same = 0;
			foreach (Transform neighbor in neighbors) {
				if (neighbor.GetComponent<IngredientScript>().type == type) {
					same++;
					neighbor.GetComponent<IngredientScript>().Remove ();
				}
			}
			if (same >= 1) {
				Debug.Log ("Score!");
				Remove ();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (c.gameObject.tag == "Plate" && !placed && !destroyed) {
			this.transform.position = new Vector3(2.5f, 4.5f, 0.0f);
			placed = true;
		}
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.gameObject.tag == "Garbage") {
			Destroy (this.gameObject);
			parentScript.ingredients.Remove (this.transform);
			parentScript.ResetIngredients();
		}
	}

	void OnMouseDown() {
		if (!destroyed && !placed) {
			destroyed = true;
			this.rigidbody2D.velocity += new Vector2(-5.0f, 10.0f);
			this.rigidbody2D.angularVelocity += 180.0f;
			this.gameObject.collider2D.isTrigger = true;
		}
	}

	public void Spawn() {
		this.transform.position = new Vector3(-10.0f, -1.4f, 0.0f);
		placed = false;
		destroyed = false;
		type = Random.Range (0, 6);
		this.GetComponent<MeshRenderer>().material = colors[type];
	}

	public void Remove() {
		destroyed = true;
		this.transform.position = new Vector3(-15.0f, 0.0f, 0.0f);
	}
}
