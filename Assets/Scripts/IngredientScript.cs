using UnityEngine;
using System.Collections;

public class IngredientScript : MonoBehaviour {
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
	}
}
