using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHeal : MonoBehaviour {

	public int healAmount;
	private GameController gameController;

	void Start () {
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			gameController.healPlayer (healAmount);
			Destroy (this.gameObject);
		}
	}
}
