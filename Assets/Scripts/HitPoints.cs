using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour {

	public int maxHitPoints;
	private int currentHitPoints;
	public int points;
	public GameObject deathAnimation;

	// Use this for initialization
	void Start () {
		this.currentHitPoints = maxHitPoints;
	}
	
	public void Damage (int damage)
	{
		Debug.Log(this.gameObject.ToString() + " took " + damage + " damage");
		this.currentHitPoints -= damage;
		Debug.Log(this.gameObject.ToString() + " has " + currentHitPoints + " HP remaining");
		if (this.currentHitPoints <= 0) {
			this.currentHitPoints = 0;
			GameObject.FindWithTag ("GameController").GetComponent<GameController> ().addScore (points);
			this.Die ();
		}
	}

	private void Die ()
	{
		if (this.deathAnimation != null)
			Instantiate (deathAnimation, transform.position, transform.rotation);
		
		Destroy (this.gameObject);
	}
}
