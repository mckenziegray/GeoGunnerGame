using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpeedPowerup : MonoBehaviour {

	public float speedIncrease;
	public int durationSeconds;
	private GameController gameController;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PowerupManager> ().increasePlayerSpeed (speedIncrease, durationSeconds);
			Destroy (this.gameObject);
		}
	}
}
