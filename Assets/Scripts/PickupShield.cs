using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupShield : MonoBehaviour {

	private GameController gameController;

	void Start () {
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			gameController.giveShield ();
			Destroy (this.gameObject);
		}
	}
}
