using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFireRatePowerup : MonoBehaviour {

	public float shotDelayReduction; //Amount in seconds by which to decrease the time between shots
	public int durationSeconds;
	private GameController gameController;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PowerupManager> ().increaseFireRate (shotDelayReduction, durationSeconds);
			Destroy (this.gameObject);
		}
	}
}
