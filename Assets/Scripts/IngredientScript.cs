using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientScript : MonoBehaviour {
	public float speed = 1.0f;
    public bool placed = false;
	public bool destroyed = false;
	public int type = 0;
	public List<Material> colors;

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
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (c.gameObject.tag == "Plate" && !placed && !destroyed) {
			this.transform.position = new Vector3(2.5f, 4.5f, 0.0f);
			placed = true;
		}
		if (c.gameObject.tag == "Garbage") {
			Spawn ();
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
}
