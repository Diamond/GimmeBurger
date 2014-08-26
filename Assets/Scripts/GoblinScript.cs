using UnityEngine;
using System.Collections;

public class GoblinScript : MonoBehaviour {
	public float lastAttempt      = 0.0f;
	public float attemptInterval  = 0.0f;
	public float sabotageInterval = 3.0f;
	public float lastSabotage     = 0.0f;
	public bool  started          = false;
	public bool  messingWithPlate = false;
	private Transform _plate;
	public float speed = 3.0f;
	public bool gotPunched = false;

	// Use this for initialization
	void Start () {
		Reset ();
		_plate = GameObject.FindGameObjectWithTag("Plate").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (gotPunched) {
			if (this.transform.position.x <= -13.0f) {
				Reset ();
				return;
			}
		}
		if (!started && !gotPunched) {
			if (Time.time - lastAttempt >= attemptInterval) {
				started = true;
			}
		}
		if (messingWithPlate) {
			if (Time.time - lastSabotage >= sabotageInterval) {
				_plate.rigidbody2D.velocity += new Vector2(0.0f, 3.0f);
				_plate.rigidbody2D.angularVelocity += (Random.Range (-1, 1) * 90.0f);
				lastSabotage = Time.time;
				Debug.Log ("fuckin with the plate");
			}
		}
		if (started && !messingWithPlate) {
			this.transform.position += new Vector3(speed * Time.deltaTime, 0.0f);
		}
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.gameObject.tag == "Plate") {
			messingWithPlate = true;
		}
	}

	void GetPunched() {
		this.rigidbody2D.velocity -= new Vector2(20.0f, 0.0f);
		this.rigidbody2D.angularVelocity -= 180.0f;
		messingWithPlate = false;
		started = false;
		gotPunched = true;
	}

	void Reset() {
		attemptInterval = Random.Range (4.0f, 7.0f);
		lastAttempt = Time.time;
		this.transform.position = new Vector3(-13.0f, -3.0f, 5.0f);
		this.rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
		this.rigidbody2D.angularVelocity = 0.0f;
		messingWithPlate = false;
		started = false;
		gotPunched = false;
		this.transform.rotation = Quaternion.identity;
	}

	void OnMouseDown() {
		GetPunched ();
	}
}
