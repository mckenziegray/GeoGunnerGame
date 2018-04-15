using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDamage : MonoBehaviour {

	public int damageAmount;

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Hazard") {
			other.gameObject.GetComponent<HitPoints> ().Damage (damageAmount);
			Destroy (this.gameObject);
		}
	}
}
