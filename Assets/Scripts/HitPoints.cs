﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour {

	public int maxHP;
	private int currentHP;
	public bool invincible;
	public int points;
	public GameObject deathAnimation;

	// Use this for initialization
	void Start () {
		this.currentHP = maxHP;
	}
	
	public void Damage (int damage)
	{
		if (!invincible) {
			Debug.Log (this.gameObject.ToString () + " took " + damage + " damage");
			this.currentHP -= damage;
			Debug.Log (this.gameObject.ToString () + " has " + currentHP + " HP remaining");
			if (this.currentHP <= 0) {
				this.currentHP = 0;
				GameObject.FindWithTag ("GameController").GetComponent<GameController> ().addScore (points);
				this.Die ();
			}
		}
	}
	public void Heal(int healing)
	{
		this.currentHP += healing;
		if (this.currentHP > maxHP)
			this.currentHP = maxHP;
	}

	private void Die ()
	{
		if (this.deathAnimation != null)
			Instantiate (deathAnimation, transform.position, transform.rotation);
		
		Destroy (this.gameObject);
	}
}
