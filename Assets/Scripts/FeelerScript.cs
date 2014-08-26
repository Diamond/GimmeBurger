using UnityEngine;
using System.Collections;

public class FeelerScript : MonoBehaviour {
	private IngredientScript _parentScript;

	// Use this for initialization
	void Start () {
		_parentScript = this.transform.GetComponentInParent<IngredientScript>();
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.gameObject.tag == "Food") {
			if (!_parentScript.neighbors.Contains(c.transform)) {
				_parentScript.neighbors.Add(c.transform);
			}
		}
	}

	void OnTriggerExit2D(Collider2D c) {
		if (c.gameObject.tag == "Food") {
			if (_parentScript.neighbors.Contains(c.transform)) {
				_parentScript.neighbors.Remove(c.transform);
			}
		}
	}
}
