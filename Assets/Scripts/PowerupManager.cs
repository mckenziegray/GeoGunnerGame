using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {

	public void increaseFireRate(float amount, int duration) {
		StartCoroutine (increaseFireRateCoroutine (amount, duration));
	}

	IEnumerator increaseFireRateCoroutine(float amount, int duration)
	{
		this.gameObject.GetComponent<PlayerController> ().secondsBetweenShots -= amount;
		yield return new WaitForSeconds (duration);
		this.gameObject.GetComponent<PlayerController> ().secondsBetweenShots += amount;
	}

	public void increasePlayerSpeed(float amount, int duration) {
		StartCoroutine (increasePlayerSpeedCoroutine (amount, duration));
	}

	IEnumerator increasePlayerSpeedCoroutine(float amount, int duration)
	{
		this.gameObject.GetComponent<PlayerController> ().speed += amount;
		yield return new WaitForSeconds (duration);
		this.gameObject.GetComponent<PlayerController> ().speed -= amount;
	}
}
