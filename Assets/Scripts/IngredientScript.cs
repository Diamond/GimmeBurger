using UnityEngine;
using System.Collections;

public class IngredientScript : MonoBehaviour {
	public float speed = 1.0f;
    public bool placed = false;
	public bool destroyed = false;

	// Use this for initialization
	void Start () {
		Input.simulateMouseWithTouches = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!placed && !destroyed) {
			this.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
		}
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (c.gameObject.tag == "Plate" && !placed && !destroyed) {
			this.transform.position = new Vector3(2.5f, 4.5f, 0.0f);
			placed = true;
		}
		if (c.gameObject.tag == "Garbage") {
			this.transform.position = new Vector3(-20.0f, 0.7f, 0.0f);
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
}
