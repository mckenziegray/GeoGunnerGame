using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public int damageAmount;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController>();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			gameController.damagePlayer (damageAmount);
			Destroy (this.gameObject);
		}
	}
}
